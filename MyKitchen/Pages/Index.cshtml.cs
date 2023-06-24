using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        private IMyKitchenDataService _ds;

        public  IndexModel(IMyKitchenDataService ds)
        {
            _ds = ds;

        }
        public async Task OnGetAsync()
        {
            await _ds.TestSQLConnectivity();
        }
    }
}
