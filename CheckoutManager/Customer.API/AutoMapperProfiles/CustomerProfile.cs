using AutoMapper;
using Customer.API.Command;
using Customer.API.Command.Dto;
using dbModel = Customer.API.Models;

namespace Customer.API.AutoMapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<DocumentDto, dbModel.Document>();
            CreateMap<dbModel.Document,DocumentDto>();
            CreateMap<CreateCustomerCommand, dbModel.Customer>();
            CreateMap<dbModel.Customer, CreateCustomerResult>();
        }
    }
}
