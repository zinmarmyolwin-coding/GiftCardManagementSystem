using GiftCardManagementSystem.APIGateway.Service;
using GiftCardManagementSystem.Model;
using GiftCardManagementSystem.Model.GiftCard;
using GiftCardManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GiftCardManagementSystem.APIGateway.Controllers
{
    [Authorize]
    [Route("api/service")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {

        private readonly ApiGateWayService _service;
        //private readonly IGiftCardRepository _giftCardRepository;

        public ApiGatewayController(
            ApiGateWayService service
            //IGiftCardRepository giftCardRepository
            )
        {
            _service = service;
            //_giftCardRepository = giftCardRepository;
        }

        [HttpPost]
        [Route("giftcard")]
        public async Task<IActionResult> GiftcardService([FromBody] ApiRequestModel reqModel)
        {
            var model = await _service.Service(reqModel);
            return Content(JsonConvert.SerializeObject(model) ?? throw new InvalidOperationException(), "application/json");
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpGet]
        //[Route("GiftCardlist")]
        //public GiftcardResponseModel GiftCardlist()
        //{
        //    var result = _giftCardRepository.GiftCardlist();
        //    return result;
        //}

        //[HttpGet]
        //[Route("GetByGiftCardId")]
        //public GiftcardResponseModel GetByGiftCardId(int id)
        //{
        //    var result = _giftCardRepository.GetByGiftCardId(id);
        //    return result;
        //}

        //[HttpPost]
        //[Route("CheckoutList")]
        //public CheckoutResponseModel CheckoutList(CheckoutRequestModel reqModel)
        //{
        //    var result = _giftCardRepository.CheckoutList(reqModel);
        //    return result;
        //}
    }
}
