using Customer.API.Command.Dto;
using MediatR;

namespace Customer.API.Command
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResult>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DocumentDto Document { get; set; }
    }
}
