using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;
using Restaurant.Interfaces;
using System.Data;
using System.Xml.Linq;

namespace Restaurant.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;

        public ClientService(IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _clientRepository.GetAllClients();
        }

        public async Task<ClientDto> GetClientById(int id)
        {
            var isExist = await _clientRepository.AnyClientById(id);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var clientId =  await _clientRepository.GetClientById(id);
            var clientResponse = new ClientDto
            {
                FirstName = clientId.FirstName,
                LastName = clientId.LastName,
                Email = clientId.Email,
                PhoneNumber = clientId.PhoneNumber,
                OrderHistories = clientId.OrderHistories.Select(oh => new OrderHistoryDto
                {
                    Id = oh.OrderId,
                    OrderName = oh.Order.OrderName,
                    Price = oh.Order.Price
                }).ToList(),
            };
            return clientResponse;
        }

        public async Task CreateClient(ClientCreationDto client)
        {
            var isExist = await _clientRepository.AnyClientByEmail(client.Email);
            if (isExist)
            {
                throw new InvalidOperationException();
            }

            await _clientRepository.CreateClient(new Client
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            });
        }

        public async Task<ClientDto> UpdateClient(int clientId, ClientCreationDto client)
        {
            var isExist = await _clientRepository.AnyClientById(clientId);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var isExistEmail = await _clientRepository.AnyClientByEmail(client.Email);
            if (isExistEmail)
            {
                throw new InvalidOperationException();
            }

            var newClient = new Client
            {
                Id = clientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
            };

            await _clientRepository.UpdateClient(newClient);
            Client clientResponse = await _clientRepository.GetClientById(newClient.Id);
            return new ClientDto
            {
                FirstName = clientResponse.FirstName,
                LastName = clientResponse.LastName,
                Email = clientResponse.Email,
                PhoneNumber = clientResponse.PhoneNumber,
            };
        }

        public async Task DeleteClient(int id)
        {
            var isExist = await _clientRepository.AnyClientById(id);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }
            await _clientRepository.DeleteClientById(id);
        }

        public async Task AddOrder(int clientId, int orderId)
        {
            var isExistForClientId = await _clientRepository.AnyClientById(clientId);
            if (!isExistForClientId)
            {
                throw new InvalidOperationException();
            }
            var isExistForOrderId = await _orderRepository.AnyOrderById(orderId);
            if (!isExistForOrderId)
            {
                throw new InvalidOperationException();
            }
            await _clientRepository.AddOrder(clientId, orderId);
        }


        public async Task<ClientDto> GetTotalOrderPrice(int id)
        {
            var isExist = await _clientRepository.AnyClientById(id);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var clientId = await _clientRepository.GetClientById(id);
            var totalOrderPrice = await _clientRepository.CalculateTotalOrderPrice(id);

            var clientResponse = new ClientDto
            {
                FirstName = clientId.FirstName,
                LastName = clientId.LastName,
                Email = clientId.Email,
                PhoneNumber = clientId.PhoneNumber,
                OrderHistories = clientId.OrderHistories.Select(oh => new OrderHistoryDto
                {
                    Id = oh.OrderId,
                    OrderName = oh.Order.OrderName,
                    Price = oh.Order.Price
                }).ToList(),
                TotalPrice = totalOrderPrice 
            };

            return clientResponse;
        }


    }
}
