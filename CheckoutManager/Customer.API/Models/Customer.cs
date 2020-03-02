using Service.Common.Commands.CustomerService;
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

        public Customer(CreateCustomer @command)
        {
            FirstName = @command.FirstName;
            LastName = @command.LastName;
            Document = new Document()
            {
                DocumentNumber = @command.Document.DocumentNumber,
                DocumentType = @command.Document.DocumentType,
            };
        }
    }
}
