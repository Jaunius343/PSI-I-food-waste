using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PSI_Food_waste.Pages.Forms
{
    public delegate void DiscountPrice(Product products);
    public class RestaurantVerifiedModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; }

        public List<Product> products;

        public DiscountPrice newPrice = ProductService.NewPrice;

        public string GlutenFreeText(Product product)
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
        public void OnGet()
        {
            ProductService.SortProducts();
            products = ProductService.GetList(RestaurantServices.nextID);
            //dependencie injection

        }
        public IActionResult OnPost()
        {
            if(HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
        
            ProductService.Add(NewProduct);
            newPrice.Invoke(NewProduct);
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            ProductService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
