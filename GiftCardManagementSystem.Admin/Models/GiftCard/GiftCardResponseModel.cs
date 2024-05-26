namespace GiftCardManagementSystem.Admin.Models.GiftCard
{
    public class GiftCardResponseModel : BaseResponseModel
    {
        public List<GiftCardModel> GiftCardList { get; set; }
        public GiftCardModel GiftCard { get; set; }
    }
    public class GiftCardModel
    {
        public int GiftCardId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? GiftCardNo { get; set; }
        public string ExpiryDate { get; set; }
        public decimal? Amount { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
