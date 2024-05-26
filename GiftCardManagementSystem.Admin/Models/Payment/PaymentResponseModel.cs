namespace GiftCardManagementSystem.Admin.Models.Payment
{
    public class PaymentResponseModel : BaseResponseModel
    {
       public List<PaymentModel> PaymentList { get; set; }
    }
    public class PaymentModel
    {
        public string PaymentMethodName { get; set; }
        public decimal Discount { get; set; }
    }
}
