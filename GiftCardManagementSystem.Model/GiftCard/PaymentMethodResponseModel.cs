using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class PaymentMethodResponseModel : BaseSubResponseModel
    {
        public List<PaymentMethodModel> PaymentMethodList { get; set; }
    }

    public class PaymentMethodModel
    {
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Discount { get; set; }
    }
}
