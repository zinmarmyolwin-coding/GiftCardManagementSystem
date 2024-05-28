using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class GiftcardModel
    {
        public int GiftCardId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? GiftCardNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public decimal? Amount { get; set; }

        public int Quantity { get; set; }
        public int CashbackPoint { get; set; }
        public decimal CashbackAmount { get; set; }
       
        public bool? IsActive { get; set; }
        public bool? IsOutOfStock { get; set; }
    }
}
