using MediatR;
using Service.Common.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using DBModels = Customer.API.Models;

namespace Customer.API.Command.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private IRepository<DBModels.Customer> _customerRepository;
        public CreateCustomerHandler(IRepository<DBModels.Customer> customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            DBModels.Customer customer = new DBModels.Customer(request);
            var result = await _customerRepository.CreateAsync(customer);
            return new CreateCustomerResult(result);
        }
    }
}
