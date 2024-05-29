using Microsoft.AspNetCore.Mvc.Rendering;

namespace GiftCardManagementSystem.Admin.Models.Payment
{
    public class PaymentMethodModel
    {
        public List<SelectListItem> Items { get; set; }
    }
}
