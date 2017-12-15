namespace Demo.WebApi.Controllers
{
    using Demo.Entities;
    using Demo.WebApi.Models;

    using Ottoman.Core.Infrastructure;

    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The order controller.
    /// </summary>
    public class OrderController : OttomanController<Order, OrderDto, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="genericRepository">
        /// The generic repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public OrderController(IRepositoryAsync<Order> genericRepository, IUnitOfWorkAsync unitOfWork)
            : base(genericRepository, unitOfWork)
        {
        }
    }
}
