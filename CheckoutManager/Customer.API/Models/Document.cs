using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Models
{
    public class Document
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
}
