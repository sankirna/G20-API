using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments.POSPayment
{
    public class POSPaymentInfoValidator
    {
        public POSPaymentInfoValidator() { }

        public List<string> Validate(PaymentInfoModel paymentInfoModel)
        {
            List<string> errors = new List<string>();
            if(string.IsNullOrEmpty( paymentInfoModel.POSTransactionId))
            {
                errors.Add("POS transactionId is required");
            }
            return errors;
        }
    }
}
