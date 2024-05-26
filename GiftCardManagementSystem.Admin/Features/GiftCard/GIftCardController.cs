using GiftCardManagementSystem.Admin.Models.GiftCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.Admin.Features.GiftCard
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _giftCardService;

        public PaymentController(PaymentService giftCardService)
        {
            _giftCardService = giftCardService;
        }

        [HttpGet]
        public ActionResult GiftCardList()
        {
            var result = _giftCardService.GiftCardList();
            return View(result);
        }

        [HttpPost]
        public ActionResult GiftCardCreate(GiftCardRequestModel reqModel)
        {
            var result = _giftCardService.GiftCardCreate(reqModel);
            return View(result);
        }


        [HttpPost]
        public ActionResult GiftCardById(GiftCardRequestModel reqModel)
        {
            var result = _giftCardService.GiftCardById(reqModel);
            return View();
        }

        [HttpPost]
        public ActionResult GiftCardEdit(GiftCardRequestModel reqModel)
        {
            var result = _giftCardService.GiftCardEdit(reqModel);
            return View(result);
        }
    }
}
