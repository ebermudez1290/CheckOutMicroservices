using Checkout.Service.Models;
using Checkout.Service.PaymentGateway;
using Microsoft.Extensions.Logging;
using RawRabbit;
using Service.Common.Events;
using Service.Common.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Service.Common.Enums.ServiceEnums;

namespace Checkout.Service.Handlers
{
    public class PostedOrderHandler : IEventHandler<PostedOrder>
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly ILogger<PostedOrderHandler> _logger;
        private readonly IPaymentGateway _gateway;
        private readonly IBusClient _busClient;

        public PostedOrderHandler(ILogger<PostedOrderHandler> logger, IRepository<Payment> paymentRepository, IBusClient busClient, IPaymentGateway gateway)
        {
           _paymentRepository = paymentRepository;
            _logger = logger;
            _gateway = gateway;
            _busClient = busClient;
        }

        public async Task HandleAsync(PostedOrder @event)
        {
            try
            {
                _logger.LogError($"We received the following order : {@event.OrderId}");
                Thread.Sleep(30000);
                var result = await _gateway.ProcessPaymentAsync(false);
                var payment = new Payment(@event);
                payment=_paymentRepository.Create(payment);
                if (result)
                    await _busClient.PublishAsync(new PaymentAccepted() {
                        Amount = payment.Amount,
                        OrderId = payment.OrderId,
                        PaymentId = payment.Id,
                        Type = PaymentType.CreditCard
                    });
                else
                    await _busClient.PublishAsync(new PaymentRejected() {
                        OrderId = payment.OrderId,
                        Code = "401",
                        Reason = "The payment cannot be processed right now"
                    });
        }
            catch (ArgumentNullException exception)
            {
                _logger.LogError($"There was an error publishing : {exception.Message}");
                await _busClient.PublishAsync(new PaymentRejected() {
                    OrderId = @event.OrderId,
                    Code = "400",
                    Reason = exception.Message
                });
            }
        }
    }
}
