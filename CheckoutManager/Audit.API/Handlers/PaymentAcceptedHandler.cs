using Auditservice;
using Microsoft.Extensions.Logging;
using Service.Common.Events;
using Service.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.Common.Enums.ServiceEnums;

namespace Audit.API.Handlers
{
    public class PaymentAcceptedHandler : IEventHandler<PaymentAccepted>
    {
        private readonly ILogger<PaymentAcceptedHandler> _logger;
        IRepository<AuditEntry> _auditRepository;
        public PaymentAcceptedHandler(ILogger<PaymentAcceptedHandler> logger, IRepository<AuditEntry> auditRepository)
        {
            _logger = logger;
            _auditRepository = auditRepository;
        }

        public async Task HandleAsync(PaymentAccepted @event)
        {
            await Task.Run(() => _logger.LogInformation("We are receiving a payment Accepted system"));
            _auditRepository.Create(new AuditEntry()
            {
                OrderId = @event.OrderId,
                PaymentId = @event.PaymentId,
                Amount = @event.Amount,
                Code = 200,
                Description = $"The payment was accepted for order {@event.OrderId}",
                Status = PaymentStatus.Accepted.ToString(),
            });
        }
    }
}
