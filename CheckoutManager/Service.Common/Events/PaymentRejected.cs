using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common.Events
{
    public class PaymentRejected : IRejectedEvent
    {
        public long OrderId { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }
    }
}
