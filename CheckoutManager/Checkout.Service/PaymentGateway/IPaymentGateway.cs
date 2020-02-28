using System.Threading.Tasks;

namespace Checkout.Service.PaymentGateway
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPaymentAsync(bool throwException);
        bool ProcessPayment(bool throwException);
    }
}
