using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateCustomerHandler(IRepository<DBModels.Customer> customerRepository, IMapper mapper)
        {
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }
        public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            DBModels.Customer customer = _mapper.Map<CreateCustomerCommand,DBModels.Customer>(command);
            var result = await _customerRepository.CreateAsync(customer);
            return _mapper.Map<DBModels.Customer, CreateCustomerResult>(result);
        }
    }
}
