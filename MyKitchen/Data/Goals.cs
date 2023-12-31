using System.ComponentModel.DataAnnotations;


namespace MyKitchen.Data
{
    public class Goals{
        [Key]
        public int GoalID {get; set;}
        public string Goal {get; set;}
        
    }




}
