using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Data;

namespace MyKitchen.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyKitchen.Data.ApplicationDbContext _context;

        public EditModel(MyKitchen.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FoodGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodGroupExists(FoodGroup.FoodGroupID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodGroupExists(int id)
        {
            return _context.FoodGroups.Any(e => e.FoodGroupID == id);
        }
    }
}
