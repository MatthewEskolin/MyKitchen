using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyKitchen.Pages
{
    public interface IGroceryListService {
        public Task<List<GroceryListItem>> GetGroceryListForUserAsync();
    }
}
