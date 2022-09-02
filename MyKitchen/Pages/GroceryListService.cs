using System.Collections.Generic;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public class GroceryListService : IGroceryListService {

        private ApplicationUser User { get;  set; }

        public GroceryListService(ApplicationUser user){

            this.User = user;
        }

        public async Task AddItemForUserAsync(){

            await Task.CompletedTask;

            return ;
        }

        public async Task<List<GroceryListItem>> GetGroceryListForUserAsync()
        {
            var rtn = new List<GroceryListItem>(){
                new GroceryListItem(){ Item = "Green Olives"  },
                new GroceryListItem(){ Item = "Radish"  },
                new GroceryListItem(){ Item = "Beets"  }
           };

           await Task.CompletedTask;

           return rtn;

        }


    }
}
