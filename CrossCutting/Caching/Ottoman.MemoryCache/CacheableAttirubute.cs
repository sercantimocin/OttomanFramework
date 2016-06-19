namespace Ottoman.MemoryCache
{
    using System;
    using System.Reflection;
    using System.Text;

    using PostSharp.Aspects;

    [Serializable]
    public class CacheableAttirubute : OnMethodBoundaryAspect
    {
        // This field will be set by CompileTimeInitialize and serialized at build time, 
        // then deserialized at runtime.
        private string methodName;

        // Method executed at build time.
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            if (method.DeclaringType != null)
            {
                this.methodName = method.DeclaringType.FullName + "." + method.Name;
            }
        }

        private string GetCacheKey(object instance, Arguments arguments)
        {
            // If we have no argument, return just the method name so we don't uselessly allocate memory.
            if (instance == null && arguments.Count == 0)
            {
                return this.methodName;
            }

            // Add all arguments to the cache key. Note that generic arguments are not part of the cache
            // key, so method calls that differ only by generic arguments will have conflicting cache keys.
            StringBuilder stringBuilder = new StringBuilder(this.methodName);
            stringBuilder.Append('(');
            if (instance != null)
            {
                stringBuilder.Append(instance);
                stringBuilder.Append("; ");
            }

            for (int i = 0; i < arguments.Count; i++)
            {
                stringBuilder.Append(arguments.GetArgument(i) ?? "null");
                stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }

        // This method is executed before the execution of target methods of this aspect.
        public override void OnEntry(MethodExecutionArgs args)
        {
            // Compute the cache key.
            string cacheKey = this.GetCacheKey(args.Instance, args.Arguments);

            // Fetch the value from the cache.
            object value = MemoryCacheManager.Instance.Get(cacheKey);

            if (value != null)
            {
                // The value was found in cache. Don't execute the method. Return immediately.
                args.ReturnValue = value;
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
            {
                // The value was NOT found in cache. Continue with method execution, but store
                // the cache key so that we don't have to compute it in OnSuccess.
                args.MethodExecutionTag = cacheKey;
            }
        }

        // This method is executed upon successful completion of target methods of this aspect.
        public override void OnSuccess(MethodExecutionArgs args)
        {
            string cacheKey = (string)args.MethodExecutionTag;

            MemoryCacheManager.Instance.Set(cacheKey, args.ReturnValue, 0);
        }
    }
}
