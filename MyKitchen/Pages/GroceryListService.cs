using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models.BL;
using Microsoft.EntityFrameworkCore;

namespace MyKitchen.Pages
{
    public class GroceryListService : IGroceryListService {

        private UserInfo User { get;  set; }
        protected IMyKitchenDataContext Context {get; set;}

        public List<GroceryListItem> GroceryList { get; set; }

        public GroceryListService(UserInfo user, IMyKitchenDataContext ctx){

            this.User = user;
            this.Context = ctx;
        }

        public async Task AddItemForUserAsync(GroceryListItem item){

            await Context.GroceryListItems.AddAsync(item);
            await Context.SaveChangesAsync();

            //Update Service List with change
            this.GroceryList = Context.GroceryListItems.ToList();;

            return;
        }



        public  async Task<List<GroceryListItem>> LoadGroceryList()
        {
            //If list needs to  be refreshed from DB, load from DB - else use Context
            if (this.GroceryList == null) { }


            Context.GroceryListItems.Where(x => x.UserID == this.User.User.Id);
            //Loads from Db

            var rtn = new List<GroceryListItem>()
            {
                new GroceryListItem(){ Item = "Green Olives" ,GroceryListItemID = -1 },
                new GroceryListItem(){ Item = "Radish"  , GroceryListItemID = -2},
                new GroceryListItem(){ Item = "Beets",GroceryListItemID = -3  }
            };

            this.GroceryList = rtn;

            await Task.CompletedTask;

            return rtn;

        }


        public async Task ShopItem(GroceryListItem item)
        {

            var id = item.GroceryListItemID;
            MyKitchen.Data.GroceryListItem gItem = await Context.GroceryListItems.Where(x => x.GroceryListItemID == id).FirstOrDefaultAsync();
            if (gItem != null) gItem.Shopped = true;
            await Context.SaveChangesAsync();

            this.GroceryList = Context.GroceryListItems.ToList();

        }

        public async Task DeleteItem(GroceryListItem item)
        {
            var id = item.GroceryListItemID;
            MyKitchen.Data.GroceryListItem gItem = await Context.GroceryListItems.Where(x => x.GroceryListItemID == id).FirstOrDefaultAsync();
            if (gItem != null) Context.GroceryListItems.Remove(gItem);
            await Context.SaveChangesAsync();

            this.GroceryList = Context.GroceryListItems.ToList();
        }
    }
}
