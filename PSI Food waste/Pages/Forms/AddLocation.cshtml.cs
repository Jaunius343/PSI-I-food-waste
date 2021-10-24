using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            RestaurantListModel.UserLocation = UserLocation.City;
            return RedirectToPage("/Forms/RestaurantList", new { UserLocation.City });
        }
    }
}
