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

        public List<GroceryListItem> GroceryItems {get; set;} = new();

        public GroceryListService(UserInfo user, IMyKitchenDataContext ctx){

            this.User = user;
            this.Context = ctx;
        }

        public async Task AddItemForUserAsync(GroceryListItem item){

            await Context.GroceryListItems.AddAsync(item);
            await Context.SaveChangesAsync();

            //Update Service List with change
            this.GroceryItems = Context.GroceryListItems.ToList();;

            return;
        }

        public async Task<List<GroceryListItem>> GetGroceryListForUserAsync()
        {
            //Loads from Db

            var rtn = new List<GroceryListItem>()
            {
                new GroceryListItem(){ Item = "Green Olives" ,GroceryListItemID = -1 },
                new GroceryListItem(){ Item = "Radish"  , GroceryListItemID = -2},
                new GroceryListItem(){ Item = "Beets",GroceryListItemID = -3  }
            };

            this.GroceryItems = rtn;

            await Task.CompletedTask;

            return rtn;

        }


        public async Task ShopItem(GroceryListItem item)
        {

            var id = item.GroceryListItemID;
            MyKitchen.Data.GroceryListItem gItem = await Context.GroceryListItems.Where(x => x.GroceryListItemID == id).FirstOrDefaultAsync();
            gItem.Shopped = true;
            await Context.SaveChangesAsync();

            this.GroceryItems = Context.GroceryListItems.ToList();

        }

        public async Task DeleteItem(GroceryListItem item)
        {
            await Task.CompletedTask;
        }
    }
}
