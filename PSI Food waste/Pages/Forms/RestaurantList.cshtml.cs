using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;

namespace PSI_Food_waste.Pages.Forms
{
    public class RestaurantListModel : PageModel
    {
        public List<Restaurant> restaurants { get; set; }
        public List<Product> products { get; set; }

        [BindProperty]
        public string SearchByCity { get; set; }

        public string UserLocation { get; set; }

        [BindProperty]
        public string msg { get; set; }

        [BindProperty]
        public bool Login { get; set; }

        public IRestaurantRepository _restaurantRepository;

        public IRegisterRepository _registerRepository;
        public IProductRepository _productRepository;

        public RestaurantListModel(IRestaurantRepository restaurantRepository, IRegisterRepository registerRepository, IProductRepository productRepository)
        {
            _restaurantRepository = restaurantRepository;
            _registerRepository = registerRepository;
            _productRepository = productRepository;
        }

        public void OnGet(string City)
        {
                ViewData["User"] = HttpContext.Session.GetString(key: "username");
            SearchByCity = City;
            restaurants = _restaurantRepository.GetAll();
            products = _productRepository.GetAll();

            if (SearchByCity is null || SearchByCity.Equals("None"))
            {

            }
            else
            {
                restaurants = restaurants.Where((restaurants, SearchByCity) => restaurants.City.Equals(SearchByCity), SearchByCity);
            }
        }
        public void OnPostFilter(LocationModel loc)
        {
            products = _productRepository.GetAll();
            restaurants = _restaurantRepository.GetAll();
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            if (SearchByCity == "None")
            {

            }
            else
            {
                restaurants = restaurants.Where((restaurants, SearchByCity) => restaurants.City.Equals(SearchByCity), SearchByCity);
            }
        }
        public IActionResult OnPostSelect(Guid id)
        {
            return RedirectToPage("/Forms/RestaurantProducts", new { ID = id });
        }
        public IActionResult OnPostSubscribe(Guid id)
        {
            if (_registerRepository.CurrentUser.Username != null)
            {
                Restaurant restaurant = new Restaurant();
                restaurant = _restaurantRepository.Get(id);
                if (!_registerRepository.CurrentUser.SubscribedRestaurants.Contains(restaurant))
                {
                    _registerRepository.CurrentUser.SubscribedRestaurants.Add(restaurant);
                    return RedirectToAction("Get", new LocationModel { City = SearchByCity });
                }
                else
                    return RedirectToAction("OnPostFilter");
            }
            else
            {
                return RedirectToAction("OnPostFilter");
            }
        }
    }
}
