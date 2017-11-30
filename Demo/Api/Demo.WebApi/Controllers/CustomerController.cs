namespace Demo.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Entities;
    using Models;

    using Ottoman.Mapper.Extensions;
    using Ottoman.MemoryCache;

    using Repository.Pattern.UnitOfWork;

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
            _customerService = customerService;
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

        //[Cacheable(60)]
        [HttpGet]
        public CustomerDto Get(int id)
        {
            var c = this._customerService.Find(id);
            return c.To<CustomerDto>();
        }

        [HttpPost]
        public void Post([FromBody] CustomerDto customer)
        {
            _customerService.Insert(customer.To<Customer>());
            //_unitOfWork.SaveChangesAsync();
        }

        [HttpPut]
        public void Put([FromBody] CustomerDto customer)
        {
            _customerService.Update(customer.To<Customer>());
            //_unitOfWork.SaveChangesAsync();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _customerService.Delete(id);
            //_unitOfWork.SaveChangesAsync();
        }
    }
}
