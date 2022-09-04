using System.Collections.Generic;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public interface IGroceryListService {

        public Task<List<GroceryListItem>> GetGroceryListForUserAsync();
        public Task ShopItem(GroceryListItem item);
        public Task DeleteItem(GroceryListItem item);
    }
}
