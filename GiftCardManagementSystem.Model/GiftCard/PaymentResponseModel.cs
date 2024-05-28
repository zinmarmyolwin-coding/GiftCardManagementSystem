using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class PaymentResponseModel : BaseSubResponseModel
    {
        public decimal? TotalAmount { get; set; }
        public decimal? CashbackAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? NetAmount { get; set; }

    }
    public class PaymentRequestModel : BaseSubRequestModel
    {
        public string ToUser { get; set; }
        public bool IsSelf { get; set; }
        public string PaymentCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CashbackAmount { get; set; }
        public int CashbackPoint { get; set; }
        public string TranId { get; set; }
        //public List<CheckoutTransactionModel> CheckoutList { get; set; }
    }

    public class CheckoutTransactionModel
    {
        public string TranId { get; set; }
    }
}
