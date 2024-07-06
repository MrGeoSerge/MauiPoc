using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TokenReceiverWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IHubContext<DataHub> _hubContext;

        public TokenController(IHubContext<DataHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DataModel dataModel)
        {
            var token = dataModel.Token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Data cannot be null or empty.");
            }

            await _hubContext.Clients.All.SendAsync("ReceiveData", token);

            return Ok($"Received data: {token}");
        }

        public class DataModel
        {
            public string Token { get; set; }
        }
    }
}
