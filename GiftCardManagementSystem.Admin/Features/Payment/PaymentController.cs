using GiftCardManagementSystem.Admin.Features.GiftCard;
using GiftCardManagementSystem.Admin.Models.Admin;
using GiftCardManagementSystem.Admin.Models.GiftCard;
using GiftCardManagementSystem.Admin.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult PaymentList()
        {
            var model = new PaymentResponseModel();
            model = _paymentService.PaymentList();
            return View(model);
        }

        [HttpGet]
        public IActionResult PaymentCreate()
        {
            ViewBag.PaymentMethodList = PaymentMethodList();
            return View();
        }

        public PaymentMethodModel PaymentMethodList()
        {
            var model = new PaymentMethodModel();
            model.Items = new List<SelectListItem>()
            {
                new SelectListItem { Value = "KPAY", Text = "KBZ PAY" },
                new SelectListItem { Value = "AYAPAY", Text = "AYA PAY" },
                new SelectListItem { Value = "WAVEMONEY", Text = "WAVE MONEY" }
            };

            return model;
        }

        [HttpPost]
        public IActionResult PaymentCreate(PaymentRequestModel reqModel)
        {
            reqModel.PaymentMethodName = PaymentMethodList().Items.FirstOrDefault(x => x.Value == reqModel.PaymentMethodCode).Text!;
            if (!ModelState.IsValid)
            {
                var result = _paymentService.PaymentCreate(reqModel);
                if (result.Response.IsError)
                {
                    ViewBag.ErrorMessage = result.Response.RespDesp;
                    goto Result;
                }
                return RedirectToAction("PaymentList", "Payment");

            }
            Result:
            ViewBag.PaymentMethodList = PaymentMethodList();
            return View(reqModel);
        }
    }
}
