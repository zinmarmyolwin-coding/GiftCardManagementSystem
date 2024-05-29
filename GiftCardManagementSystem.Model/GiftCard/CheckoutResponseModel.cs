using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class CheckoutResponseModel : BaseSubResponseModel
    {
        public List<CheckoutModel> ckeckoutList { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CashbackAmount { get; set; }
        public int CashbackPoint{ get; set; }
        public string TranId { get; set; }
    }

    public class CheckoutModel
    {
        public string GiftCardCode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CashbackPoint { get; set; }
        public decimal? CashbackAmount { get; set; }
    }
}
