using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;
using Restaurant.Interfaces;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<List<Client>> GetAllClients()
        {
            return await _clientService.GetAllClients();
        }

        [HttpGet("{id:int}")]
        public async Task<ClientDto> GetClientById([FromRoute]int id)
        {
            return await _clientService.GetClientById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody]ClientCreationDto client)
        {
            await _clientService.CreateClient(client);
            return Ok();
        }

        [HttpPut("{clientId:int}/")]
        public async Task<ActionResult<ClientDto>> UpdateClient([FromRoute] int clientId, [FromBody] ClientCreationDto client)
        {
            return new OkObjectResult(await _clientService.UpdateClient(clientId, client));
        }

        [HttpDelete("{clientId:int}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int clientId)
        {
            await _clientService.DeleteClient(clientId);
            return Ok();
        }

        [HttpPost("{clientId:int}/order/{orderId:int}")]
        public async Task<IActionResult> CreateClient([FromRoute]int clientId, [FromRoute]int orderId)
        {
            await _clientService.AddOrder(clientId, orderId);
            return Ok();
        }

        [HttpGet("{clientId:int}/totalOrderPrice")]
        public async Task<ClientDto> GetTotalOrderPrice(int clientId)
        {
            return await _clientService.GetTotalOrderPrice(clientId);
        }
    }
}
