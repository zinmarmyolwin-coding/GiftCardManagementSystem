using GiftCardManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GiftCardManagementSystem.APIGateway.Controllers
{
    [Authorize]
    [Route("api/service")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {

        private readonly GiftCardService giftCardService;

        public ApiGatewayController(GiftCardService giftCardService)
        {
            this.giftCardService = giftCardService;
        }

        // GET: api/<ApiGatewayController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApiGatewayController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApiGatewayController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiGatewayController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiGatewayController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
