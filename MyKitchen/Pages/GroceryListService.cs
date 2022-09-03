using System.Collections.Generic;
using System.Threading.Tasks;
using MyKitchen.Data;
using MyKitchen.Models.BL;

namespace MyKitchen.Pages
{
    public class GroceryListService : IGroceryListService {

        private UserInfo User { get;  set; }

        public GroceryListService(UserInfo user){

            this.User = user;
        }

        public async Task AddItemForUserAsync(){

            await Task.CompletedTask;

            return ;
        }

        public async Task<List<GroceryListItem>> GetGroceryListForUserAsync()
        {
            var rtn = new List<GroceryListItem>(){
                new GroceryListItem(){ Item = "Green Olives" ,GroceryListItemID = -1 },
                new GroceryListItem(){ Item = "Radish"  , GroceryListItemID = -2},
                new GroceryListItem(){ Item = "Beets",GroceryListItemID = -3  }
           };

           await Task.CompletedTask;

           return rtn;

        }


    }
}
