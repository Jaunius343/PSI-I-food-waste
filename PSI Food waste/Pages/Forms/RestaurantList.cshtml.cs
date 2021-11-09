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

        [BindProperty]
        public string SearchByCity { get; set; }

        public static string UserLocation { get; set; }

        [BindProperty]
        public string msg { get; set; }

        [BindProperty]
        public bool Login {  get; set; }

        public IRestaurantRepository _restaurantRepository;

        public RestaurantListModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }


        public void OnGet()
        {

          // LoginScreenModel.CurrentUser.SubscribedRestaurants.Add(new Restaurant { Title = "Picerija", Adress = "Sudu gatve", City = "Vilnius", Id = 20, WorkerID = 1});
            if (LoginScreenModel.CurrentUser.Username != null)
            {
                Login = true;
            }
                ViewData["User"] = HttpContext.Session.GetString(key: "username");
            SearchByCity = UserLocation;
            restaurants = _restaurantRepository.GetAll();

            if (SearchByCity == "None")
            {

            }
            else
            {
                restaurants = restaurants.Where((restaurants, SearchByCity) => restaurants.City.Equals(SearchByCity), SearchByCity);
            }
        }
        public void OnPostFilter()
        {
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
        public IActionResult OnPostSelect(int id)
        {
            RestaurantProductsModel.IdTest = id;
            return RedirectToPage("/Forms/RestaurantProducts");
        }
        public IActionResult OnPostSubscribe(int id)
        {
            if (LoginScreenModel.CurrentUser.Username != null)
            {
                Login = true;
                Restaurant restaurant = new Restaurant();
                restaurant = _restaurantRepository.Get(id);
                LoginScreenModel.CurrentUser.SubscribedRestaurants.Add(restaurant);
                msg = "You are subscribed to this restaurant";
                return RedirectToAction("OnPostFilter");
            }
            else
            {
                Login = false;
                msg = "Login to subscribe";
                return RedirectToAction("OnPostFilter");
            }
        }
    }
}
