using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public class BaseModel : PageModel
    {
        public string SystemMessage {get; set;}
    }
    public class GroceryListModel :BaseModel 
    {
        //TODO should list be stored in session so we don't have to keep loading it twice on every post?

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
            await LoadGroceryList();
            return;

        }

        public async Task OnPostSubmit(int id)
        {
            await LoadGroceryList();
            var item = this.GroceryList.Where(x => x.GroceryListItemID == id).FirstOrDefault();

            await GroceryListSvc.ShopItem(item);

            await LoadGroceryList();
            SystemMessage = $"Submited ID {id}";

        }

        public async Task OnPostDelete(int id)
        {
            await Task.CompletedTask;

            SystemMessage = $"Deleted ID {id}";
            await LoadGroceryList();
        }

        private async Task LoadGroceryList()
        {
            GroceryList = await GroceryListSvc.GetGroceryListForUserAsync();

        }
    }
}
