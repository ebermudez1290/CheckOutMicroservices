using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.API.Models
{
    public class Order
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public IEnumerable<Item> ItemList { get; set; }
        public string Code { get; set; }
        public string Version { get; set; }
        public string Channel { get; set; }
        public string AppId { get; set; }
        public decimal Total { get; set; }
        public string ShippingId { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status{ get; set; }
    }
}
