using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.Model
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public decimal? Discount { get; set; }
    }
}
