using Service.Common.Events;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Service.Common.Enums.ServiceEnums;

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

        public Payment() { }

        public Payment(PostedOrder postedOrder)
        {
            OrderId = postedOrder.OrderId;
            Type = "Credit Card";
            TenderAmount = postedOrder.Total;
            Change = 0;
            Amount = postedOrder.Total;
        }
    }
}
