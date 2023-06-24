using System.Threading.Tasks;

namespace MyKitchen.Services;

///
///
public interface IMyKitchenDataService
{
    //Settings
    Task UpdateSettingsAsync(string settings);
    Task TestSQLConnectivity();
}