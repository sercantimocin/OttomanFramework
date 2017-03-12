namespace Demo.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Entities;
    using Models;

    using Ottoman.Mapper.Extensions;
    using Ottoman.MemoryCache;

    using Service.Pattern;

    /// <summary>
    /// The customer controller.
    /// </summary>
    public class CustomerController : ApiController
    {
        /// <summary>
        /// The _customer service.
        /// </summary>
        private readonly IService<Customer> _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="customerService">
        /// The customer service.
        /// </param>
        public CustomerController(IService<Customer> customerService)
        {
            this._customerService = customerService;
        }

        /// GET: api/Default
        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Customer> Get()
        {
            return this._customerService.Queryable();
        }

        // GET: api/Default/5
        [Cacheable(cacheDuration: 60)]
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
