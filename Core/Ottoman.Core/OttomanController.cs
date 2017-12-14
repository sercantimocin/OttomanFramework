namespace Ottoman.Core
{
    using System.Linq;
    using System.Web.Http;

    using Ottoman.Mapper.Extensions;

    using Repository.Pattern.Ef6;
    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The ottoman controller.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TResult">
    /// </typeparam>
    public class OttomanController<TEntity, TResult> : ApiController where TEntity : Entity
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

        [HttpPost]
        public void Post([FromBody] TResult genericObject)
        {
            _genericRepository.Insert(genericObject.To<TEntity>());
            _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="genericObject">
        /// The generic object.
        /// </param>
        [HttpPut]
        public void Put([FromBody] TResult genericObject)
        {
            _genericRepository.Update(genericObject.To<TEntity>());
            _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [HttpDelete]
        public void Delete(int id)
        {
            _genericRepository.Delete(id);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
