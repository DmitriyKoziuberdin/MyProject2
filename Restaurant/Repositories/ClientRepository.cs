using Microsoft.EntityFrameworkCore;
using Restaurant.AppDb;
using Restaurant.Entity;
using Restaurant.Interfaces;
using System.Data;

namespace Restaurant.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            var getClient = await _context.Clients
                .Include(orderHistory => orderHistory.OrderHistories)
                .ThenInclude(order => order.Order)
                .FirstAsync(clientId => clientId.Id == id);
            return getClient;
        }

        public async Task CreateClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteClientById(int id)
        {
            var deletingClient = await _context.Clients
                .Where(clientId => clientId.Id == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return deletingClient;
        }

        public async Task<bool> AnyClientById(int clientId)
        {
            return await _context.Clients.AnyAsync(id => id.Id == clientId);
        }

        public async Task<bool> AnyClientByEmail(string emaiL)
        {
            return await _context.Clients.AnyAsync(email => email.Email == emaiL);
        }

        public async Task AddOrder(int clientId, int orderId)
        {
            _context.Set<OrderHistory>().Add(new OrderHistory
            {
                ClientId = clientId,
                OrderId = orderId
            });
            await _context.SaveChangesAsync();
        }


        public async Task<decimal> CalculateTotalOrderPrice(int clientID)
        {
            var client = await _context.Clients
                .Include(orderHistory => orderHistory.OrderHistories)
                .ThenInclude(order => order.Order)
                .FirstAsync(clientId => clientId.Id == clientID);

            decimal totalOrderPrice = client.OrderHistories.Sum(orderHistory => orderHistory.Order.Price);
            return totalOrderPrice;
        }
    }
}
