using Auditservice;
using Microsoft.Extensions.Logging;
using Service.Common.Events;
using Service.Common.Repository;
using System.Threading.Tasks;
using static Service.Common.Enums.ServiceEnums;

namespace Audit.API.Handlers
{
    public class PaymentRejectedHandler : IEventHandler<PaymentRejected>
    {
        private readonly ILogger<PaymentRejectedHandler> _logger;
        IRepository<AuditEntry> _auditRepository;

        public PaymentRejectedHandler(ILogger<PaymentRejectedHandler> logger, IRepository<AuditEntry> auditRepository)
        {
            _logger = logger;
            _auditRepository = auditRepository;
        }
        public async Task HandleAsync(PaymentRejected @event)
        {
            await Task.Run(() => _logger.LogInformation("We are receiving a payment rejected system"));
            _auditRepository.Create(new AuditEntry()
            {
                OrderId = @event.OrderId,
                PaymentId = -1,
                Amount = @event.Amount,
                Code = 200,
                Description = $"The payment was accepted for order {@event.OrderId}",
                Status = PaymentStatus.Rejected.ToString(),
            });
        }
    }
}
