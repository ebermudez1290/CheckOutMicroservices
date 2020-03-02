using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RawRabbit;
using Service.Common.Commands;
using Service.Common.Commands.CustomerService;
using Service.Common.Repository;
using DBModel = Customer.API.Models;

namespace Customer.API.EventHandlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusClient _busClient;
        private readonly IRepository<DBModel.Customer> _customerRepository;
        private ILogger<CreateCustomerHandler> _logger;

        public CreateCustomerHandler(IBusClient busClient, IRepository<DBModel.Customer> customerRepository,
            ILogger<CreateCustomerHandler> logger)
        {
            this._busClient = busClient;
            this._customerRepository = customerRepository;
            this._logger = logger;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            try
            {
                DBModel.Customer customer = new DBModel.Customer(command);
                customer = await _customerRepository.CreateAsync(customer);
                _logger.LogInformation($"Customer has been inserted with the following Id {customer.Id}");
            }
            catch (DbException exception)
            {
                _logger.LogInformation($"There was an error with the following message {exception.Message}");
            }
        }
    }
}
