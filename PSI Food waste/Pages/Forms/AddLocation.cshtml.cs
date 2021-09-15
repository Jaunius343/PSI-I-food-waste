using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;

namespace PSI_Food_waste.Pages.Forms
{
    public class AddLocationModel : PageModel
    {
        [BindProperty]
        public LocationModel UserLocation { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            return RedirectToPage("/Index", new { UserLocation.City });
        }
    }
}
