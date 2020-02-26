namespace Checkout.Service
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string[] AllowedAuthOrigins { get; set; }
    }
}
