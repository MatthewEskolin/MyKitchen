using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MyKitchen.Data.ApplicationDbContext _context;

        public CreateModel(MyKitchen.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodGroup FoodGroup { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodGroups.Add(FoodGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
