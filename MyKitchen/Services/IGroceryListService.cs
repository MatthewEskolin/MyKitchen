using System.Collections.Generic;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Services
{
    public interface IGroceryListService
    {

        public List<GroceryListItem> GroceryList { get; set; }
        public Task<List<GroceryListItem>> LoadGroceryList();
        public Task ShopItem(GroceryListItem item);
        public Task DeleteItem(int id);
        public Task AddItem(GroceryListItem item);
    }

}
