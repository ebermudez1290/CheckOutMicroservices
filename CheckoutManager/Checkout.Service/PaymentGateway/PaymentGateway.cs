using System;
using System.Threading.Tasks;

namespace Checkout.Service.PaymentGateway
{
    public class PaymentGateway : IPaymentGateway
    {

        public PaymentGateway() { }

        public async Task<bool> ProcessPaymentAsync(bool throwException)
        {
            if (throwException)
                throw new ArgumentException("The Payment could not be processed because there are no funds");
            else
                return await Task.Run(() => true);
        }

        public bool ProcessPayment(bool throwException)
        {
            if (throwException)
                throw new ArgumentException("The Payment could not be processed because there are no funds");
            else
                return true;
        }

    }
}
