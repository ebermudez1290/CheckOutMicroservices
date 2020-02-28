using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders.API.Models;
using RawRabbit;
using Service.Common.Enums;
using Service.Common.Events;
using Service.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IRepository<Order> _orderRepository;
        private readonly IBusClient _busClient;
        public OrdersController(IRepository<Order> orderRepository, IBusClient busClient)
        {
            this._busClient = busClient;
            _orderRepository = orderRepository;
        }

        #region Queries
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_orderRepository.ListAllAsync().ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            return Ok(await _orderRepository.GetByIdAsync(id));
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            var orderCreated = _orderRepository.Create(order);
            PostedOrder command = new PostedOrder()
            {
                CreateDate = order.CreateDate,
                Currency = order.Currency,
                CustomerId = order.CustomerId,
                LastUpdateDate = DateTime.Now,
                OrderId = 500,
                Total = order.Total,
                Status = ServiceEnums.OrderStatus.Pending.ToString(),
            };
            await this._busClient.PublishAsync(command);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, Order order)
        {
            return Ok(_orderRepository.Update(order));
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Order order)
        {
            _orderRepository.Delete(order);
            return Ok();
        }
    }
}
