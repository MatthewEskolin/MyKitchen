using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKitchen.Data;

namespace MyKitchen.Controllers
{
    public interface IMealRepository
    {
        Task<int> Add(Meal foodItem);

        Task<Meal> Find(int id);
        Task SaveChangesAsync();
        void Update(Meal foodItem);
        void Remove(Meal foodItem);
        Meal GetRandomItem();
        IEnumerable<Meal> GetMeals();
        int Count();
    }
}