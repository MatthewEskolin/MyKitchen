using Xunit;

namespace MyKitchen.Tests
{

    [Trait("Category","Unit")]
    public class GridUtilitiesTests
    {

        [Fact]
        public void ToggleAscDesc_Test_AscToDesc()
        {
            var toggleToDesc = MyKitchen.Utilities.GridUtilities.ToggleAscDesc("TestColumn");
            Assert.EndsWith(toggleToDesc,"_desc");
        }

        [Fact]
        public void ToggleAscDesc_Test_DescToAsc()
        {
            var toggleToDesc = MyKitchen.Utilities.GridUtilities.ToggleAscDesc("TestColumn_desc");
            Assert.False(toggleToDesc.EndsWith("_desc"),toggleToDesc);

        }


    }







}