using Microsoft.Extensions.Logging;
using Service.Common.Events;
using System;

namespace Service.Common.Exceptions
{
    public class PaymentRejectedException : Exception
    {
        public PaymentRejectedException(Exception exception, PaymentRejected paymentEvent, ILogger logger = null) :base(paymentEvent.Reason, exception)
        {
            logger.LogError(exception.Message);

        }
    }
}
