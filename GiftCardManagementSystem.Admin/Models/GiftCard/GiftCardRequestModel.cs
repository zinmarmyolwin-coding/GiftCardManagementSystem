using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.GiftCard
{
    public class GiftCardRequestModel
    {
        public int GiftCardId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? GiftCardNo { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        public decimal? Amount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public int CashbackPoint { get; set; }
        public decimal CashbackAmount { get; set; }
    }
}
