using GiftCardManagementSystem.Admin.Models.GiftCard;
using GiftCardManagementSystem.Admin.Models.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.Admin.Features.Payment
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult PaymentList()
        {
            var result = _paymentService.PaymentList();
            return View(result);
        }

        [HttpPost]
        public ActionResult PaymentCreate(PaymentRequestModel reqModel)
        {
            var result = _paymentService.PaymentCreate(reqModel);
            return View(result);
        }
    }
}
