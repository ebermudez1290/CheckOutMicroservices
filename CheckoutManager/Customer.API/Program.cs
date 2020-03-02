﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Common.Commands.CustomerService;
using Service.Common.Serilog;
using Service.Common.Services;

namespace Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(ServiceHost.Create<Startup>(args)
                .UseRabbitMq().SubscribeToCommand<CreateCustomer>()
                .Build().Run);
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
