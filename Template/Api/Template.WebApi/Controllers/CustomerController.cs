using System.Collections.Generic;
using System.Web.Http;

namespace Template.WebApi.Controllers
{
    using Ottoman.Entities;
    using Ottoman.Mapper.Extensions;

    using Service.Pattern;

    using Models;

    using Ottoman.MemoryCache;
    public class CustomerController : ApiController
    {
        private readonly IService<Customer> _customerService;

        public CustomerController(IService<Customer> customerService)
        {
            this._customerService = customerService;
        }

        // GET: api/Default
        public IEnumerable<Customer> Get()
        {
            return this._customerService.Queryable();
        }

        // GET: api/Default/5
        [CacheableAttirubute]
        public CustomerDto Get(int id)
        {
            return this._customerService.Find(id).To<CustomerDto>();
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
