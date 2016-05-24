using System.Collections.Generic;
using System.Web.Http;

namespace Template.WebApi.Controllers
{
    using Ottoman.Entities;
    using Ottoman.Mapper.Extensions;

    using Service.Pattern;

    using Models;

    public class CustomerController : ApiController
    {
        private IService<Customer> _customerService;

        public CustomerController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Default
        public IEnumerable<Customer> Get()
        {
            return _customerService.Queryable();
        }

        // GET: api/Default/5
        public CustomerDTO Get(int id)
        {
            return this._customerService.Find(id).To<CustomerDTO>();
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
