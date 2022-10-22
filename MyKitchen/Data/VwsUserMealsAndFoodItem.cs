namespace MyKitchen.Data
{
    public partial class vwsUserMealsAndFoodItem
    {
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public string AppUserId { get; set; }

        //https://learn.microsoft.com/en-us/ef/core/modeling/backing-field?tabs=data-annotations
        //TODO is it possible have entity framework write to a
        //Backing fields allow EF to read and/or write to a field rather than a property.This can be useful when encapsulation in the class is being used to restrict the use of and/or enhance the semantics around access to the data by application code, but the value should be read from and/or written to the database without using those restrictions/enhancements.



        public bool? IsQueued { get; set; }


    }
}
