using System.ComponentModel;

namespace GiftCardManagementSystem.Repository.Enum
{
    public enum EumTransactionStatus
    {
        [Description("Process")] Process = 0,
        [Description("Paid")] Paid = 1,
        [Description("UnPaid")] UnPaid = 2,
    }
    public enum EumServiceName
    {
        GiftCardlist,
        GetByGiftCardId,
        CheckoutList,
        PaymentMethodList,
        ConfirmPayment,
        PurchaseHistory,
    }
    public enum EumPaymentMethod
    {
        [Description("KPAY")] KPAY,
        [Description("AYAPAY")] AYAPAY,
        [Description("WAVEMONEY")] WAVEMONEY,
    }
    public enum EumCashback
    {
        UsedCashback,
        UnUsedCashback

    }

}
