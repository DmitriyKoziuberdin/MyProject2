using Restaurant.Entity;

namespace Restaurant.Interfaces
{
    public interface IClientRepository
    {
        public Task<List<Client>> GetAllClients();
        public Task<Client> GetClientById(int id);
        public Task CreateClient(Client client);
        public Task UpdateClient(Client client);
        public Task<int> DeleteClientById(int id);
        public Task<bool> AnyClientById(int clientId);
        public Task<bool> AnyClientByEmail(string emaiL);
        public Task AddOrder(int clientId, int orderId);

        public Task<decimal> CalculateTotalOrderPrice(int clientID);
    }
}
