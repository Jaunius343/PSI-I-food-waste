using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_Food_waste.Pages.Forms
{
    public class RestaurantProductsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);
        public List<Product> Data { get; set; }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;


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

        public async Task OnGetAsync()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            restaurant = _restaurantRepository.Get(id : IdTest);
            products = await _productRepository.GetList(id : IdTest);
            Data = await _productRepository.GetPaginatedResult(products, CurrentPage, PageSize);
            Count = products.Count;
        }
        public async Task OnPostAsync()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            restaurant = _restaurantRepository.Get(id: IdTest);
            products = await _productRepository.GetList(id: IdTest);
            Data = await _productRepository.GetPaginatedResult(products, CurrentPage, PageSize);
            Count = products.Count;
        }
        public string GlutenFreeText(Product product) 
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
    }
}
