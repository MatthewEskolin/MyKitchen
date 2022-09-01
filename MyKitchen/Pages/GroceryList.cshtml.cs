using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{

    public class GroceryListItem{
        public string Item {get; set;}
    }

    public class GroceryListModel : PageModel
    {
        public string NewGroceryItem {get; set;}
        public void OnGet()
        {

        }



    }

    public interface IGroceryListService {

    }

    public class GroceryListService : IGroceryListService {


        public GroceryListService(){}

        public async Task AddItemAsync(){
            return ;
        }


    }
}
