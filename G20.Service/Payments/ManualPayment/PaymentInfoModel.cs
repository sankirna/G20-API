﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments.ManualPayment
{
    public record PaymentInfoModel /*: BaseNopModel*/
    {
        public PaymentInfoModel()
        {
            CreditCardTypes = new List<SelectListItem>();
            ExpireMonths = new List<SelectListItem>();
            ExpireYears = new List<SelectListItem>();
        }

        public string CreditCardType { get; set; }

        public IList<SelectListItem> CreditCardTypes { get; set; }

        public string CardholderName { get; set; }

        public string CardNumber { get; set; }

        public string ExpireMonth { get; set; }

        public string ExpireYear { get; set; }

        public IList<SelectListItem> ExpireMonths { get; set; }

        public IList<SelectListItem> ExpireYears { get; set; }

        public string CardCode { get; set; }
    }
}
