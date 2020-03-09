using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels = Customer.API.Models;

namespace Customer.API.Command
{
    public class CreateCustomerResult
    {
        public long Id { get; set; }
        public CreateCustomerResult() { }
        public CreateCustomerResult(DBModels.Customer customer)
        {
            this.Id = customer.Id;
        }
    }
}
