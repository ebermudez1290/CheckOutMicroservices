using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common.Enums
{
    public static class ServiceEnums
    {
        public enum OrderStatus
        {
            Pending,
            Processed,
            Completed
        }

        public enum PaymentType
        {
            Cash,
            CreditCard,
            Credit
        }
    }
}
