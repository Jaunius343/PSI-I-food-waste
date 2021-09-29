using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;
namespace PSI_Food_waste.Pages.Forms
{
    public class AddWorkerModel : PageModel
    {
        [BindProperty]
        public Restaurant NewRestaurant {  get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            RestaurantServices.Add(NewRestaurant);
            return RedirectToPage("/Forms/RestaurantVerified");
        }
    }
}
