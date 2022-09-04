using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKitchen.Data
{
    public class GroceryListItem{

        [Key]
        public int GroceryListItemID {get; set;}
        public string Item {get; set;}
        public bool Shopped {get; set;}

        [ForeignKey("User")]
        public int UserID { get; set; }
        public ApplicationUser User {get; set;}

    }
}
