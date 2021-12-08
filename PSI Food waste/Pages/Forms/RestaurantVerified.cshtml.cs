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
using AspNetCoreHero.ToastNotification.Abstractions;

namespace PSI_Food_waste.Pages.Forms
{

    public class RestaurantVerifiedModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; }

        public List<Product> products;
        public RegisteredUser<RegisterForm> RegisteredUsers {  get; set; }
        public Restaurant currentRestaurant {  get; set; }
        [BindProperty]
        public static string Msg { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public Guid ID {  get; set; }

        public Action<Product> DiscountPrice; //= _productRepository.NewPrice;  //TODO: fix error

        public IRegistrationEventNotifier _eventNotifier;

        public  readonly INotyfService _notyf;

        public IRestaurantRepository _restaurantRepository;

        public IProductRepository _productRepository;

        public INotificationEvent _notificationEvent;

        public IRegisterRepository _registerRepository;


        public RestaurantVerifiedModel(IProductRepository productRepository, IRestaurantRepository restaurantRepository, INotificationEvent notificationEvent, IRegisterRepository registerRepository, IRegistrationEventNotifier eventNotifier, INotyfService notyf)
        {
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            DiscountPrice = _productRepository.NewPrice;
            _notificationEvent = notificationEvent;
            _registerRepository = registerRepository;
            _eventNotifier = eventNotifier;
            _notyf = notyf;
        }

        public string GlutenFreeText(Product product)
        {
            if (product.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
        public async Task OnGetAsync(Guid id)
        {
            //ID = id;
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            products = await _productRepository.GetList(id);
            currentRestaurant = _restaurantRepository.Get(id);
            try
            {
                _productRepository.SortProducts();
            }
            catch(ProductListNotFoundException)
            {

            }
        }
        public IActionResult OnPost(Guid id)
        {
            currentRestaurant = _restaurantRepository.Get(ID);
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _notificationEvent.RaiseEvent(this,NewProduct.Name, _notyf,0);
            _productRepository.Add(NewProduct, currentRestaurant.Id);
            DiscountPrice.Invoke(NewProduct);
            RegisteredUsers = _registerRepository.GetAll();
            for (int i = 0; i < RegisteredUsers.Length(); i++)
            {
                if (RegisteredUsers[i].SubscribedRestaurants.Contains(currentRestaurant))
                { 
                    string s = string.Format("A new product has been added to {0}!", currentRestaurant.Title);
                    Task t1;
                    t1 = Task.Run(() => _eventNotifier.RaiseEvent(this, new EmailNotificationArgs(RegisteredUsers[i].Email, s)));
                    t1.Wait();
                }
            }
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(Guid id)
        {     
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Forms/LoginScreen");
            }
            Product DelProduct = _productRepository.Get(id);
            _notificationEvent.RaiseEvent(this, DelProduct.Name, _notyf, 1);
            _productRepository.Delete(id);
            return Page();//RedirectToAction("Get");
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
