using DismasDemo.Data;
using DismasDemo.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DismasDemo.Hubs
{
    public class PriceUpdateHub : Hub
    {
        InventoryRepository inventoryRepository;
        private readonly InventoryContext _dbContext;

        public PriceUpdateHub(IConfiguration configuration, InventoryContext dbContext)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbContext = dbContext;
            inventoryRepository = new InventoryRepository(connectionString, _dbContext);
        }

        public async Task SendProducts()
        {
            var inventory = inventoryRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedInventory", inventory);
        }
    }
}
