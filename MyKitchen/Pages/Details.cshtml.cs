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
    public class DetailsModel : PageModel
    {
        private readonly MyKitchen.Data.ApplicationDbContext _context;

        public DetailsModel(MyKitchen.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public FoodGroup FoodGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FoodGroup = await _context.FoodGroups.FirstOrDefaultAsync(m => m.FoodGroupID == id);

            if (FoodGroup == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
