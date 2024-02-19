using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKitchen.Data
{
    public class GroceryListItem{

        [Key]
        public int GroceryListItemID {get; set;}

        [MaxLength(100)]
        public string Item {get; set;}
        public bool Shopped {get; set;}

        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUser User {get; set;}

    }
}
