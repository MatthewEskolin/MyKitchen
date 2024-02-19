using MyKitchen.Data;
using Xunit;

namespace MyKitchen.Tests
{
    public class FoodItemTests
    {
        [Fact]
        public void CanChangeFoodItemName()
        {
            var foodItem = new FoodItem();

            foodItem.FoodItemName = "Chicken";

            Assert.Equal("Chicken", foodItem.FoodItemName);

        }
    }

    
}
