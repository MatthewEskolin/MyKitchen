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

        /// <summary>
        /// should mirror list in Service
        /// </summary>
        public List<GroceryListItem> GroceryList {get; set;}

        public string NewItem {get; set;}



        IGroceryListService GroceryListSvc;

        public GroceryListModel(IGroceryListService glService)
        {
            GroceryListSvc = glService;


            //Initialize Service
            LoadGroceryList().GetAwaiter().GetResult();

            //can we keep this updated?
            this.GroceryList = GroceryListSvc.GroceryList;
        }

        public async Task OnGetAsync()
        {
        }

        public async Task OnPostSubmit(int id)
        {

            var item = this.GroceryList.FirstOrDefault(x => x.GroceryListItemID == id);

            await GroceryListSvc.ShopItem(item);


            SystemMessage = $"Submited ID {id}";

        }

        public async Task OnPostDelete(int id)
        {
            await Task.CompletedTask;

            SystemMessage = $"Deleted ID {id}";
            await LoadGroceryList();
        }

        private async Task<List<GroceryListItem>> LoadGroceryList()
        {

            GroceryList = await this.GroceryListSvc.LoadGroceryList();
            return GroceryList;

        }
    }
}
