using GiftCardManagementSystem.Admin.Models.GiftCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.Admin.Features.GiftCard
{
    public class GiftCardController : Controller
    {
        private readonly GiftCardService _giftCardService;

        public GiftCardController(GiftCardService giftCardService)
        {
            _giftCardService = giftCardService;
        }

        [HttpGet]
        public IActionResult GiftCardList()
        {
            var model = new GiftCardResponseModel();
            model = _giftCardService.GiftCardList();
            return View(model);
        }

        [HttpGet]
        public IActionResult GiftCardCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GiftCardCreate(GiftCardRequestModel reqModel)
        {
            if (ModelState.IsValid)
            {
                var result = _giftCardService.GiftCardCreate(reqModel);
                if (result.Response.IsError)
                {
                    ViewBag.ErrorMessage = result.Response.RespDesp;
                    return View(reqModel);
                }
                return RedirectToAction("GiftCardList", "GiftCard");
            }
          
            return View(reqModel);
        }


        [HttpGet]
        public IActionResult GiftCardEdit(int id)
        {
            var model = new GiftCardResponseModel();
            model = _giftCardService.GiftCardById(id);
            return View(model.GiftCard);
        }

        [HttpPost]
        public IActionResult GiftCardEdit(GiftCardRequestModel reqModel)
        {
            if (ModelState.IsValid)
            {
                var result = _giftCardService.GiftCardEdit(reqModel);
                if (result.Response.IsError)
                {
                    ViewBag.ErrorMessage = result.Response.RespDesp;
                    return View(reqModel);
                }
                return RedirectToAction("GiftCardList", "GiftCard");
            }
            return View(reqModel);
        }
    }
}
