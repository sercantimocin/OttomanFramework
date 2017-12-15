namespace Ottoman.Core
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Ottoman.Core.Data;
    using Ottoman.Mapper.Extensions;

    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The ottoman controller.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TResult">
    /// </typeparam>
    public class OttomanController<TEntity, TResult, TKey> : ApiController where TEntity : BaseEntity<TKey> where TKey : struct
    {
        /// <summary>
        /// The _generic repository.
        /// </summary>
        private readonly IRepositoryAsync<TEntity> _genericRepository;

        /// <summary>
        /// The _unit of work.
        /// </summary>
        private readonly IUnitOfWorkAsync _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanController{TEntity,TResult}"/> class.
        /// </summary>
        /// <param name="genericRepository">
        /// The generic repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public OttomanController(IRepositoryAsync<TEntity> genericRepository, IUnitOfWorkAsync unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TResult> Get()
        {
            return _genericRepository.Queryable().To<TEntity, TResult>();
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        [HttpGet]
        public TResult Get(int id)
        {
            return _genericRepository.Find(id).To<TResult>();
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="genericObject">
        /// The generic object.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public Task<int> Post([FromBody] TResult genericObject)
        {
            _genericRepository.Insert(genericObject.To<TEntity>());
            return _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="genericObject">
        /// The generic object.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPut]
        public Task<int> Put(TKey id, [FromBody] TResult genericObject)
        {
            TEntity entity = genericObject.To<TEntity>();
            entity.Id = id;
            _genericRepository.Update(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpDelete]
        public Task<int> Delete(int id)
        {
            _genericRepository.Delete(id);
            return _unitOfWork.SaveChangesAsync();
        }
    }
}
