using AutoMapper;
using Customer.API;
using Customer.API.AutoMapperProfiles;
using Customer.API.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Service.Common.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using DbModels = Customer.API.Models;

namespace Customer.Tests.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        Mock<IRepository<DbModels.Customer>> _mockRepository;
        Mock<IMediator> _mockMediator;
        private IMapper _mapper;
        private CancellationToken _defaultCancelToken = default(CancellationToken);

        [OneTimeSetUp]
        public void setup()
        {
            _mockRepository = new Mock<IRepository<DbModels.Customer>>();
            _mockMediator = new Mock<IMediator>();
            var config = new MapperConfiguration(opts => { opts.AddProfile(new CustomerProfile()); });
            _mapper = new Mapper(config);
        }

        [Test]
        public async Task CustomerController_CallAPostWithException_ExpectA500ErrorAndSendMediatorToBeCalled()
        {
            var controller = new CustomersController(_mockRepository.Object, _mockMediator.Object);
            var command = new CreateCustomerCommand()
            {
                FirstName = "Erick",
                LastName = "Bermudez",
                Document = new API.Command.Dto.DocumentDto()
                {
                    Number = "1426",
                    Type = "cedula"
                }
            };
            _mockMediator.Setup(x => x.Send(command, default(CancellationToken))).Throws<ArgumentException>();
            var customer = _mapper.Map<CreateCustomerCommand, DbModels.Customer>(command);
            var result = await controller.Post(command);
            var codeId = result as ObjectResult;
            Assert.That(codeId.StatusCode, Is.EqualTo(500));
            _mockRepository.Verify(x => x.CreateAsync(customer), Times.Never);
            _mockMediator.Verify(x => x.Send(command, _defaultCancelToken));
        }

        [Test]
        public async Task CustomerController_CallAPostSuccessFully_ExpectA200ErrorAndSendMediatorToBeCalled()
        {
            var controller = new CustomersController(_mockRepository.Object, _mockMediator.Object);
            var command = new CreateCustomerCommand()
            {
                FirstName = "Erick",
                LastName = "Bermudez",
                Document = new API.Command.Dto.DocumentDto()
                {
                    Number = "1426",
                    Type = "cedula"
                }
            };
            var customer = _mapper.Map<CreateCustomerCommand, DbModels.Customer>(command);
            var result = await controller.Post(command);
            var codeId = result as OkObjectResult;
            Assert.That(codeId, Is.Not.Null);
            _mockRepository.Verify(x => x.CreateAsync(customer), Times.Never);
            _mockMediator.Verify(x => x.Send(command, _defaultCancelToken));
        }

    }
}
