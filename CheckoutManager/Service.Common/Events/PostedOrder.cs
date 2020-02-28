using System;

namespace Service.Common.Events
{
    public class PostedOrder : IEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Status { get; set; }
    }
}
