using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class TranHistoryResponseModel : BaseSubResponseModel
    {
        public List<TranHistoryModel> TranHistoryList { get; set; }
    }
    public class TranHistoryModel()
    {
        public string TranId { get; set; }
        public string UserName { get; set; }
        public string TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CashbackAmount { get; set; }
        public string PaymentName { get; set; }

    }

    public class TranHistoryRequestModel : BaseSubRequestModel
    {
        public string Cashback { get; set; }
    }
}
