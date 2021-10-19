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
            new User {Username = "admin", Password = "admin"},
            new User {Username = "abc", Password = "123"}
        };

        public RegisteredUser<RegisterForm> RegUsers { get; set; }

        public void OnGet()
        {
            //RegUsers = RegisterService.GetAll();
            Msg = "";
        }
        public bool flag;
        public IActionResult OnPost()
        {
            RegUsers = RegisterService.GetAll();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            flag = false;

            for (int i = 0; i < RegUsers.Length(); i++)
            {
                if (NewUser.Username.Equals(RegUsers[i].Username))
                {
                    if (NewUser.Password.Equals(RegUsers[i].Password))
                    {
                        HttpContext.Session.SetString("username", NewUser.Username);
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
                HttpContext.Session.SetString("username", NewUser.Username);
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
