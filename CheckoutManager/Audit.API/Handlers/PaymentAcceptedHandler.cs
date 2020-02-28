using Microsoft.Extensions.Logging;
using Service.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.API.Handlers
{
    public class PaymentAcceptedHandler : IEventHandler<PaymentAccepted>
    {
        private readonly ILogger _logger;
        public PaymentAcceptedHandler(ILogger logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(PaymentAccepted @event)
        {
            await Task.Run(() => _logger.LogInformation("We are receiving a payment Accepted system"));
        }
    }
}
