using System;
using System.Linq;
using MyKitchen.Data;
using Xunit;

namespace MyKitchen.Tests.Integration
{
    public class RegistrationTests :IClassFixture<_SharedTestContext>{
        
            private readonly _SharedTestContext _fixture;

            public RegistrationTests(_SharedTestContext fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public async void RegisterNewUser1()
            {
                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com",UserName = $"{guid}@test.com"};
                var result =  _fixture.TestUserManager.CreateAsync(newUser, "aA11111!").GetAwaiter().GetResult();

                Assert.True(result.Succeeded);

            }

            [Fact]
            public void RegisterNewUser2()
            {
                //append guid so test coan be repeated
                var guid = Guid.NewGuid().ToString();
                var newUser = new ApplicationUser(){Email = $"{guid}@test.com", UserName=$"{guid}@test.com"};
                var result =  _fixture.TestUserManager.CreateAsync(newUser, "aA11111!").GetAwaiter().GetResult();

                Assert.True(result.Succeeded);


            }



            [Fact]
            public void DeleteUser()
            {
                //append guid so test coan be repeated
                var deleteUser = _fixture.TestUserManager.FindByEmailAsync("mjeskolin@gmail.com").GetAwaiter().GetResult();
                var result = _fixture.TestUserManager.DeleteAsync(deleteUser).GetAwaiter().GetResult();
                Assert.True(result.Succeeded);
            }



            [Fact]
            public void FoodItemsAreUserIsolated()
            {

                var guid1 = Guid.NewGuid();
                var guid2 = Guid.NewGuid();

                Assert.True(true);
                if(_fixture.ApDbContext.Users.Count() < 2){
                    Assert.Fail("Inconclusive - need 2 users in DB");
                }

                var users = _fixture.ApDbContext.Users.Take(2).ToList();

                //get user 1
                //get user 2
                var user1 = _fixture.TestUserManager.FindByIdAsync(users[0].Id).GetAwaiter().GetResult();
                var user2 = _fixture.TestUserManager.FindByIdAsync(users[1].Id).GetAwaiter().GetResult();;

                //food item 1
                //food item 2
                var foodItem1 = new FoodItem(){
                    FoodItemName = guid1.ToString(),
                    FoodDescription = "Test Item 3" + guid1
                };

                var foodItem2 = new FoodItem(){
                    FoodItemName = guid2.ToString(),
                    FoodDescription = "Test Item 3" + guid2
                };

                //add food item for user 1
                _fixture.TestFoodItemRepository.Add(foodItem1).GetAwaiter().GetResult();
                _fixture.TestFoodItemRepository.AddFoodForUser(user1,foodItem1).GetAwaiter().GetResult();

                //get food items for user 2
                _fixture.TestFoodItemRepository.Add(foodItem2).GetAwaiter().GetResult();;
                _fixture.TestFoodItemRepository.AddFoodForUser(user2,foodItem2).GetAwaiter().GetResult();

                //Assert
                Assert.True(_fixture.ApDbContext.FoodItems.Any(x => x.FoodItemID == foodItem1.FoodItemID));
                Assert.True(_fixture.ApDbContext.FoodItems.Any(x => x.FoodItemID == foodItem2.FoodItemID));

                //Assert User Food Items
                Assert.True(_fixture.ApDbContext.UserFoodItems.Any(x => x.FoodItemID == foodItem1.FoodItemID && x.AppUser.Id == user1.Id));
                Assert.True(_fixture.ApDbContext.UserFoodItems.Any(x => x.FoodItemID == foodItem2.FoodItemID && x.AppUser.Id == user2.Id));
    }
}
}
