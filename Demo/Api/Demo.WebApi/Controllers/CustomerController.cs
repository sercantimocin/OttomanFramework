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
        [Cacheable(60)]
        public CustomerDto Get(int id)
        {
            return this._customerService.Find(id).To<CustomerDto>();
        }

        // POST: api/Default
        [HttpPost]
        public void Post([FromBody] CustomerDto customer)
        {
            this._customerService.Update(customer.To<Customer>());
        }

        // PUT: api/Default/5
        [HttpPut]
        public void Put([FromBody] CustomerDto customer)
        {
            this._customerService.Insert(customer.To<Customer>());
        }

        // DELETE: api/Default/5
        [HttpDelete]
        public void Delete(int id)
        {
            this._customerService.Delete(id);
        }
    }
}
