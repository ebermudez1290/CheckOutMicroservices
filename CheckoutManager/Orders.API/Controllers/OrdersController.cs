using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders.API.Models;
using Orders.API.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ILogger<OrdersController> _logger;
        private IRepository<Order> _orderRepository;

        public OrdersController(ILogger<OrdersController> logger, IRepository<Order> orderRepository)
        {
            _logger = logger;
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
        public ActionResult<Order> Post(  Order order)
        {
            return Ok(_orderRepository.Create(order));
        }

        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id,  Order order)
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
