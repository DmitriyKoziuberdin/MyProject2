using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;

namespace Restaurant.Interfaces
{
    public interface IClientService
    {
        public Task<List<Client>> GetAllClients();
        public Task<ClientDto> GetClientById(int id);
        public Task CreateClient(ClientCreationDto client);
        public Task<ClientDto> UpdateClient(int clientId, ClientCreationDto client);
        public Task DeleteClient(int id);
        public Task AddOrder(int clientId, int orderId);

        public Task<ClientDto> GetTotalOrderPrice(int id);
    }
}
