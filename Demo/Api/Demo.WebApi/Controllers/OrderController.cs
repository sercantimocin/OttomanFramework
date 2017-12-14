using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.WebApi.Controllers
{
    using Demo.Entities;
    using Demo.WebApi.Models;

    using Ottoman.Core;

    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    public class OrderController : OttomanController<Order, OrderDto>
    {
        public OrderController(IRepositoryAsync<Order> genericRepository, IUnitOfWorkAsync unitOfWork)
            : base(genericRepository, unitOfWork)
        {
        }
    }
}
