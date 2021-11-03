using Microsoft.AspNetCore.Http;
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
        [BindProperty]
        public string searchCriteria { get; set; }

        public IRestaurantRepository _restaurantRepository;

        public IProductRepository _productRepository;

        public RestaurantProductsModel(IProductRepository productRepository, IRestaurantRepository restaurantRepository)
        {
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void OnGet()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            restaurant = _restaurantRepository.Get(id : IdTest);
            products = _productRepository.GetList(id : IdTest);
        }
        public void OnPost()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            //return RedirectToPage("/Forms/RestaurantProducts", new {searchCriteria = this.searchCriteria});
            //restaurant = RestaurantServices.Get(id: IdTest);
            restaurant = _restaurantRepository.Get(id: IdTest);
            //products = ProductService.GetList(id: IdTest);
            products = _productRepository.GetList(id: IdTest);
        }
        public string GlutenFreeText(Product product) 
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
    }
}
