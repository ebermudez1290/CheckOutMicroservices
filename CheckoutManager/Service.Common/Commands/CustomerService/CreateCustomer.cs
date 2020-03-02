using Service.Common.Commands.CustomerService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common.Commands.CustomerService
{
    public class CreateCustomer: ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DocumentDto Document { get; set; }
    }
}
