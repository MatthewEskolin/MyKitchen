using System.ComponentModel.DataAnnotations;

namespace MyKitchen.Pages
{
    public class GroceryListItem{

        [Key]
        public int GroceryListItemID {get; set;}
        public string Item {get; set;}
    }
}
