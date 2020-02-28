using Microsoft.Extensions.Logging;
using Service.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.API.Handlers
{
    public class PaymentRejectedHandler : IEventHandler<PaymentRejected>
    {
        private readonly ILogger _logger;
        public PaymentRejectedHandler (ILogger logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(PaymentRejected @event)
        {
            await Task.Run(() => _logger.LogInformation("We are receiving a payment rejected system"));
        }
    }
}
