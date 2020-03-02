using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Service.Common.Enums;
using Service.Common.Events;
using Service.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels = Customer.API.Models;

namespace Customer.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IRepository<DBModels.Customer> _customerRepository;
        public CustomersController(IRepository<DBModels.Customer> orderRepository)
        {
            _customerRepository = orderRepository;
        }

        #region Queries
        [HttpGet]
        public ActionResult<IEnumerable<DBModels.Customer>> Get()
        {
            return Ok(_customerRepository.ListAllAsync().ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DBModels.Customer>> Get(string id)
        {
            return Ok(await _customerRepository.GetByIdAsync(id));
        }
        #endregion

        [HttpPost]
        public ActionResult<DBModels.Customer> Post(DBModels.Customer customer)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<DBModels.Customer> Put(int id, DBModels.Customer customer)
        {
            return Ok(_customerRepository.Update(customer));
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] DBModels.Customer customer)
        {
            _customerRepository.Delete(customer);
            return Ok();
        }
    }
}
