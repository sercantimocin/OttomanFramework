namespace Ottoman.Mapper.Extensions
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    public static class MapperExtensions
    {
        public static T To<T>(this object obj)
        {
            return AutoMapper.Mapper.Map<T>(obj);
        }

        public static IQueryable<TResult> To<TEntity, TResult>(this IQueryable<TEntity> queryableObject)
        {
            return queryableObject.ProjectTo<TResult>();
        }
    }
}
