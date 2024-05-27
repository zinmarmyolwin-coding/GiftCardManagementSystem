using GiftCardManagementSystem.Model.GiftCard;
using GiftCardManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.APIGateway.Controllers
{
    [Authorize]
    [Route("api/service")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {

        private readonly IGiftCardRepository _giftCardRepository;

        public ApiGatewayController(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("GiftCardlist")]
        public GiftcardResponseModel GiftCardlist()
        {
            var result = _giftCardRepository.GiftCardlist();
            return result;
        }

        [HttpGet]
        [Route("GetByGiftCardId")]
        public GiftcardResponseModel GetByGiftCardId(int id)
        {
            var result = _giftCardRepository.GetByGiftCardId(id);
            return result;
        }
    }
}
