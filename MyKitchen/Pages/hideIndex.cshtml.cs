using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyKitchen.Data.ApplicationDbContext _context;

        public IndexModel(MyKitchen.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FoodGroup> FoodGroup { get;set; }

        public async Task OnGetAsync()
        {
            FoodGroup = await _context.FoodGroups.ToListAsync();
        }
    }
}
