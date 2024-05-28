using GiftCardManagementSystem.Model.GiftCard;

namespace GiftCardManagementSystem.Repository.IRepository
{
    public interface IGiftCardRepository
    {
        Task<GiftcardResponseModel> GiftCardlistAsync();
        Task<GiftcardResponseModel> GetByGiftCardIdAsync(string raw);
        Task<CheckoutResponseModel> CheckoutListAsync(string raw);
        Task<PaymentMethodResponseModel> PaymentMethodListAsync();
        Task<PaymentResponseModel> ConfirmPaymentAsync(string raw);
        Task<TranHistoryResponseModel> PurchaseHistoryAsync(string raw);
    }
}
