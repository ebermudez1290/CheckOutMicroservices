using System;
using System.Collections.Generic;
using System.Text;
using static Service.Common.Enums.ServiceEnums;

namespace Service.Common.Events
{
    public class PaymentAccepted : IEvent
    {
        public long PaymentId { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }
    }
}
