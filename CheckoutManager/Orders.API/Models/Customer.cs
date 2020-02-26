namespace Orders.API.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
}
