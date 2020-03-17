using AutoMapper;
using Customer.API;
using Customer.API.AutoMapperProfiles;
using Customer.API.Command;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Service.Common.Testing;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DbModels = Customer.API.Models;

namespace Customer.Tests.IntegrationTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) => { conf.AddJsonFile(configPath); });
            });
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CustomerController_CallAGet_ExpectListOfCustomers()
        {
            var response = await _client.DoGetAsync<List<DbModels.Customer>>("/api/Customers");
            Assert.That(response.Count, Is.Not.Null);
        }

        [Test]
        public async Task CustomerController_CreateACustomerUsingCommand_ExpectCustomerWithId()
        {
            var command = new CreateCustomerCommand()
            {
                FirstName = "Erick",
                LastName = "Bermudez",
                Document = new API.Command.Dto.DocumentDto()
                {
                    Number = "1114420653",
                    Type = "cedula"
                }
            };
            var response = await _client.DoPostAsync<CreateCustomerResult>("/api/Customers", command);
            Assert.That(response.ErrorCode, Is.Null);
            Assert.That(response.Data.Id, Is.Not.LessThan(0));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}