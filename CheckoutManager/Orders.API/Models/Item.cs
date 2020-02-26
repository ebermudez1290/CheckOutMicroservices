using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Models
{
    public class Item
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
        public bool Taxable { get; set; }
    }
}
