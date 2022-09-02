using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyKitchen.Pages
{
    public class GroceryListModel : PageModel
    {
        public List<GroceryListItem> GroceryList {get; set;} = new();
        public string NewItem {get; set;}

        IGroceryListService GroceryListSvc;

        public GroceryListModel(IGroceryListService glService)
        {
            GroceryListSvc = glService;
        }

        public async Task OnGetAsync()
        {
            //Load Users Grocery List
            GroceryList = await GroceryListSvc.GetGroceryListForUserAsync();
            return;

        }

    }
}
