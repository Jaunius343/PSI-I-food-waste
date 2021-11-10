using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Services;
using PSI_Food_waste.Models;

namespace PSI_Food_waste.Pages.Forms
{
    public class LoginScreenModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        [BindProperty]
        public string Msg { get; set; }

        public List<User> users = new List<User>
        {
            new User {Email = "admin" , Username = "admin", Password = "admin"},
            new User {Email = "abc" , Username = "abc", Password = "123"}
        };
        public RegisteredUser<RegisterForm> RegUsers { get; set; }

        public static RegisterForm CurrentUser { get; set; }

        public void OnGet()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            //RegUsers = RegisterService.GetAll();
            Msg = "";
        }
        public bool flag;
        public IActionResult OnPost()
        {
            //TODO FIX: EMPTY EMAIL FIELD WILL THROW EXCEPTION
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RegUsers = RegisterService.GetAll();
            flag = false;

            for (int i = 0; i < RegUsers.Length(); i++)
            {
                if (NewUser.Email.Equals(RegUsers[i].Email))
                { 
                    if (NewUser.Password.Equals(RegUsers[i].Password))
                    {
                        CurrentUser = RegisterService.GetUserData(RegUsers[i].Email);
                        HttpContext.Session.SetString("username", RegUsers[i].Username);
                        return RedirectToPage("/Index");
                    }
                }
            }
            foreach (User user in users)
            {
                if (NewUser.Equals(user))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                HttpContext.Session.SetString("username", "admin");
                return RedirectToPage("/Index");
            }
            else
            {
                Msg = "Incorrect credentials";
                return Page();
            }
        }
    }
}
