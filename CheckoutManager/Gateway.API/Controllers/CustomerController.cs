using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Service.Common.Commands.CustomerService;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IBusClient _busClient;
        public CustomerController(IBusClient busClient)
        {
            this._busClient = busClient;
        }

        //#region Queries
        //[HttpGet]
        //public ActionResult Get()
        //{
        //    return Ok(_customerRepository.ListAllAsync().ToList());
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<DBModels.Customer>> Get(string id)
        //{
        //    return Ok(await _customerRepository.GetByIdAsync(id));
        //}
        //#endregion

        [HttpPost]
        public async Task<ActionResult<CreateCustomer>> Post(CreateCustomer command)
        {
            await this._busClient.PublishAsync(command);
            return Accepted(command);
        }

        //[HttpPut("{id}")]
        //public ActionResult<DBModels.Customer> Put(int id, DBModels.Customer customer)
        //{
        //    return Ok(_customerRepository.Update(customer));
        //}

        //[HttpDelete]
        //public ActionResult Delete([FromBody] DBModels.Customer customer)
        //{
        //    _customerRepository.Delete(customer);
        //    return Ok();
        //}
    }
}
