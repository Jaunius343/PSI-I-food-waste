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
    public class RestaurantListModel : PageModel
    {
        public List<Restaurant> restaurants { get; set; }

        [BindProperty]
        public string SearchByCity {  get; set; }
         
        public void OnGet()
        {
            SearchByCity = "None";
            restaurants = RestaurantServices.GetAll();
        }
        public void OnPostFilter()
        {
            if (SearchByCity == "None")
            {
                restaurants = RestaurantServices.GetAll();
            }
            //else
            //{
                //var CityRestaurants = new List<Restaurant>();
                //CityRestaurants = RestaurantServices.GetAll();
               // foreach (var cityRestaurant in CityRestaurants.Where((cityRestaurant,SearchByCity) => cityRestaurant.City.Equals(SearchByCity), SearchByCity))
                //{
                  //  restaurants.Add(cityRestaurant);//new Restaurant { Adress = cityRestaurant.Adress, City = cityRestaurant.City, Id = cityRestaurant.Id, Title = cityRestaurant.Title, WorkerID = cityRestaurant.WorkerID });
                //}

            //}
            else
            {
                restaurants = RestaurantServices.GetAll();
                restaurants = restaurants.Where((restaurants, SearchByCity) => restaurants.City.Equals(SearchByCity), SearchByCity);
            }

        }
         public IActionResult OnPostSelect(int id)
        {
            RestaurantProductsModel.IdTest = id;
            return RedirectToPage("/Forms/RestaurantProducts");
        }
    }
}
