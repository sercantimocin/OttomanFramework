namespace Ottoman.Core
{
    using System.Linq;
    using System.Web.Http;

    using Ottoman.Mapper.Extensions;

    using Repository.Pattern.Ef6;
    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    public class OttomanController<TEntity, TResult> : ApiController where TEntity : Entity
    {

        private readonly IRepositoryAsync<TEntity> _genericRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;


        public OttomanController(IRepositoryAsync<TEntity> genericRepository, IUnitOfWorkAsync unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TResult> Get()
        {
            return _genericRepository.Queryable().To<TEntity, TResult>();
        }

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

        [HttpPut]
        public void Put([FromBody] TResult genericObject)
        {
            _genericRepository.Update(genericObject.To<TEntity>());
            _unitOfWork.SaveChangesAsync();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _genericRepository.Delete(id);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
