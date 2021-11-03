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

        [BindProperty]
        public static string Msg { get; set; } = "";

        public Action<Product> DiscountPrice; //= _productRepository.NewPrice;  //TODO: fix error

        public event EventHandler<ProductArgs> OnAddedProduct;
        public event EventHandler<ProductArgs> OnRemovedProduct;


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
            OnAddedProduct += RestaurantVerifiedModel_OnAddedProduct;
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
            OnAddedProduct?.Invoke(this, new ProductArgs(NewProduct.Name, "has been added to the product list"));
            return RedirectToAction("Get");
        }
        private void RestaurantVerifiedModel_OnAddedProduct(object sender, ProductArgs e)
        {
            Msg = e.Name + " " + e.Msg;
        }
        public IActionResult OnPostDelete(int id)
        {
            OnRemovedProduct += RestaurantVerifiedModel_OnRemovedProduct;
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            Product DelProduct = _productRepository.Get(id);
            OnRemovedProduct?.Invoke(this, new ProductArgs(DelProduct.Name, "was removed from the product list"));
            _productRepository.Delete(id);
            return RedirectToAction("Get");
        }

        private void RestaurantVerifiedModel_OnRemovedProduct(object sender, ProductArgs e)
        {
            Msg = e.Name + " " + e.Msg;
        }
    }
    public class ProductArgs : EventArgs
    {
        public string Msg;

        public string Name;
        public ProductArgs(string name, string msg)
        {
            Msg = msg;
            Name = name;
        }
    }
}
