namespace Demo.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Entities;
    using Models;

    using Ottoman.Core;
    using Ottoman.Mapper.Extensions;
    using Ottoman.MemoryCache;

    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    using Service.Pattern;

    /// <summary>
    /// The customer controller.
    /// </summary>
    public class CustomerController : OttomanController<Customer, CustomerDto>
    {
        public CustomerController(IRepositoryAsync<Customer> genericRepository, IUnitOfWorkAsync unitOfWork)
            : base(genericRepository, unitOfWork)
        {
        }
    }
}
