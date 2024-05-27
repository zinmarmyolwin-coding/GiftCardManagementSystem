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

        public DateTime? CreatedDate { get; set; }

        public string? CreatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedUserId { get; set; }

        public bool? IsActive { get; set; }
    }
}
