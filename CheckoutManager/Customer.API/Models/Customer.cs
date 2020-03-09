using Customer.API.Command;
using Customer.API.Command.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Models
{
    public class Customer
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Document Document { get; set; }

        public Customer() { }
        public Customer(CreateCustomerCommand command)
        {
            FirstName = command.FirstName;
            LastName = command.LastName;
            Document = new Document()
            {
                Number = command.Document.Number,
                Type = command.Document.Type
            };
        }
    }
}
