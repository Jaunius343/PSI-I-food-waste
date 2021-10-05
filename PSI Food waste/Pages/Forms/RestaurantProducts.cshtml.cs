using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;
using System.Collections.Generic;

namespace PSI_Food_waste.Pages.Forms
{
    public class RestaurantProductsModel : PageModel
    {
        public List<Product> products { get; set; }
        public Restaurant restaurant {  get; set; }
        public static int IdTest { get; set; }
        public void OnGet()
        {
            restaurant = RestaurantServices.Get(IdTest);
            products = ProductService.GetAll();
        }
        public string GlutenFreeText(Product product)
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
    }
}
