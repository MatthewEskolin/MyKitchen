using System.Linq;
using Xunit;

namespace MyKitchen.Tests.Integration;

public class DataIntegrityTests : IClassFixture<_SharedTestContext>{


        
    _SharedTestContext Fixture;

    public DataIntegrityTests(_SharedTestContext fixture)
    {
        this.Fixture = fixture;
    }

    [Fact]
    //All Foods Associated with a meal also need to be associated with the user who owns the meal.
    public void MealUsersCheck()
    {
        var badRecords = Fixture.ApDbContext.vwsMealItems.Any(x => x.UserFoodItemsAppUser == null);
        Assert.True(!badRecords);

    }


    //check changes

}