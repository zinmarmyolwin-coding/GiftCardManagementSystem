using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.Payment
{
    public class PaymentRequestModel
    {
        public string PaymentMethodName { get; set; }
        public string PaymentMethodCode { get; set; }
        public decimal Discount { get; set; }
    }
}
