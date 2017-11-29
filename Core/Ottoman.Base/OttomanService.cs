namespace Ottoman.Core
{
    using Repository.Pattern.Infrastructure;
    using Repository.Pattern.Repositories;

    using Service.Pattern;

    public class OttomanService<TEntity> : Service<TEntity> where TEntity : class, IObjectState
    {
        public OttomanService(IRepositoryAsync<TEntity> repository) : base(repository)
        {
        }
    }
}
