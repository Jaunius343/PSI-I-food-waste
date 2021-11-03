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

    public class RestaurantVerifiedModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; }

        public List<Product> products;

        public static string msg;

        public Action<Product> DiscountPrice; //= _productRepository.NewPrice;  //TODO: fix error

        public event EventHandler<AddedProductArgs> OnAddedProduct;
        public IRestaurantRepository _restaurantRepository;

        public IProductRepository _productRepository;

        public RestaurantVerifiedModel(IProductRepository productRepository, IRestaurantRepository restaurantRepository)
        {
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            DiscountPrice = _productRepository.NewPrice;
        }

        public string GlutenFreeText(Product product)
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
        public void OnGet()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            _productRepository.SortProducts();
            products = _productRepository.GetList(_restaurantRepository.GetID());

        }
        public IActionResult OnPost()
        {
            OnAddedProduct += RestaurantVerifiedModel_OnAddedProduct1;
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productRepository.Add(NewProduct);
            DiscountPrice.Invoke(NewProduct);
            OnAddedProduct?.Invoke(this, new AddedProductArgs(NewProduct.Name, "has been added to the product list"));
            return RedirectToAction("Get");
        }
        private void RestaurantVerifiedModel_OnAddedProduct1(object sender, AddedProductArgs e)
        {
            msg = e.Name + " " + e.Msg;
        }
        public IActionResult OnPostDelete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            _productRepository.Delete(id);
            return RedirectToAction("Get");
        }
    }
    public class AddedProductArgs : EventArgs
    {
        public string Msg;

        public string Name;
        public AddedProductArgs(string name, string msg)
        {
            Msg = msg;
            Name = name;
        }
    }
}
