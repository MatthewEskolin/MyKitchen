using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyKitchen.Pages
{
    public class GroceryListService : IGroceryListService {
        private readonly ILogger<GroceryListService> _logger;

        private UserInfo User { get;  set; }
        protected IMyKitchenDataContext Context {get; set;}

        public List<GroceryListItem> GroceryList { get; set; }

        public GroceryListService(ILogger<GroceryListService> _logger, UserInfo user, IMyKitchenDataContext ctx){

            this.User = user;
            this.Context = ctx;
            this._logger = _logger;
        }

        public async Task AddItem(GroceryListItem item){

            //populate user value
            item.UserID = this.User.User.Id;

            await Context.GroceryListItems.AddAsync(item);
            await Context.SaveChangesAsync();

            //Update Service List with change
            this.GroceryList = Context.GroceryListItems.ToList();;


            _logger.LogInformation($"Added {item.Item}");

            return;
        }

        public  async Task<List<GroceryListItem>> LoadGroceryList()
        {
            //If list needs to  be refreshed from DB, load from DB - else use Context
            if (this.GroceryList == null) { }


            var rtn = Context.GroceryListItems.Where(x => x.UserID == this.User.User.Id).ToList();
            //Loads from Db

            //var rtn = new List<GroceryListItem>()
            //{
            //    new GroceryListItem(){ Item = "Green Olives" ,GroceryListItemID = -1 },
            //    new GroceryListItem(){ Item = "Radish"  , GroceryListItemID = -2},
            //    new GroceryListItem(){ Item = "Beets",GroceryListItemID = -3  }
            //};

            this.GroceryList = rtn;

            await Task.CompletedTask;

            return rtn;

        }

        public async Task ShopItem(GroceryListItem item)
        {

            var id = item.GroceryListItemID;
            MyKitchen.Data.GroceryListItem gItem = await Context.GroceryListItems.Where(x => x.GroceryListItemID == id && x.UserID == this.User.User.Id).FirstOrDefaultAsync();
            if (gItem != null) gItem.Shopped = true;
            await Context.SaveChangesAsync();

            this.GroceryList = Context.GroceryListItems.ToList();

            _logger.LogInformation($"Shopped {item.Item}");

        }

        public async Task DeleteItem(int  id)
        {
            MyKitchen.Data.GroceryListItem gItem = await Context.GroceryListItems.Where(x => x.GroceryListItemID == id && x.UserID == this.User.User.Id).FirstOrDefaultAsync();
            if (gItem != null) Context.GroceryListItems.Remove(gItem);
            await Context.SaveChangesAsync();

            this.GroceryList = Context.GroceryListItems.ToList();


            _logger.LogInformation($"Deleted {gItem.Item}");

        }
    }
}
