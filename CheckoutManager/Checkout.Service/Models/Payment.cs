using System;

namespace Checkout.Service.Models
{
    public class Payment
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Type { get; set; }
        public Decimal TenderAmount { get; set; }
        public Decimal Change { get; set; }
        public Decimal Amount { get; set; }
    }
}
