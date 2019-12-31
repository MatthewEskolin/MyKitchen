using System.Collections;
using System.Collections.Generic;
using MyKitchen.Data;

namespace MyKitchen.Tests
{
    public class FoodItemTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {GetTestFoodItems()};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private FoodItem[] GetTestFoodItems()
        {
            var item = new FoodItem[]
            {

                new FoodItem() {FoodItemID = 1, FoodItemName = "Test Item 1"},
                new FoodItem() {FoodItemID = 2, FoodItemName = "Test Item 2"},
                new FoodItem() {FoodItemID = 3, FoodItemName = "Test Item 3"},
                new FoodItem() {FoodItemID = 4, FoodItemName = "Test Item 4"},
                new FoodItem() {FoodItemID = 5, FoodItemName = "Test Item 5"},
                new FoodItem() {FoodItemID = 6, FoodItemName = "Test Item 6"},
                new FoodItem() {FoodItemID = 7, FoodItemName = "Test Item 7"},
                new FoodItem() {FoodItemID = 8, FoodItemName = "Test Item 8"},
                new FoodItem() {FoodItemID = 9, FoodItemName = "Test Item 9"},
                new FoodItem() {FoodItemID = 10, FoodItemName = "Test Item 10"},

            };

            return item;

        }





    }
}