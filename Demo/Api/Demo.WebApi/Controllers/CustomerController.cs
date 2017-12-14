namespace Demo.WebApi.Controllers
{
    using Entities;
    using Models;

    using Ottoman.Core;

    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The customer controller.
    /// </summary>
    public class CustomerController : OttomanController<Customer, CustomerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="genericRepository">
        /// The generic repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public CustomerController(IRepositoryAsync<Customer> genericRepository, IUnitOfWorkAsync unitOfWork)
            : base(genericRepository, unitOfWork)
        {
        }
    }
}
