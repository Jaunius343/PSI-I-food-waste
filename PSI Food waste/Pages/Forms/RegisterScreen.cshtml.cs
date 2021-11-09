using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;

namespace PSI_Food_waste.Pages.Forms
{
    public class RegisterScreenModel : PageModel
    {
        

        public event EventHandler<string> OnSucessfullRegistrationEvent;

        public RegisteredUser<RegisterForm> RegisteredUsers { get; set; }/* = new RegisteredUser<RegisterForm>();*/

        public RegisterForm RegisteredUser { get; set; }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }
        [BindProperty]
        [Required]
        public string Pass { get; set; }
        [BindProperty]
        [Required]
        public int Num { get; set; }
        [BindProperty]
        public string Msg { get; set; }
        public string ErrorMsg { get; set; }

        public void OnGet()
        {
            ViewData["User"] = HttpContext.Session.GetString(key: "username");
            RegisteredUsers = RegisterService.GetAll();
            //if(RegisteredUsers == null)
            //{
            //RegisteredUsers = new RegisteredUser<RegisterForm>();
            //}
        }
        public IActionResult OnPost()
        {
            //email validation
            Regex regex3 = new Regex(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            // Username validation
            Regex regex = new Regex(@"^\w{3,20}$");

            // password validation
            Regex regex2 = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\@$!%*#?&])[A-Za-z\d\@$!%*#?&]{8,}$");

            RegisteredUsers = RegisterService.GetAll();
            if (Email == null || !regex3.IsMatch(Email))
            {
                ErrorMsg = "Incorrect Email";
                return Page();
            }
            for (int i = 0; i < RegisteredUsers.Length(); i++)
            {
                if (Email.Equals(RegisteredUsers[i].Email))
                {
                    ErrorMsg = "This email already exists";
                    return Page();
                }
            }
            if (Name == null || !regex.IsMatch(Name))
            { 
                ErrorMsg = "Username must contain from 3 to 20 characters with no special characters";
                return Page();
            }
            if (Pass == null || !regex2.IsMatch(Pass))
            {
                ErrorMsg = "Password must contain atleast 8 characters, one letter and a special character";
                return Page();
            }
            else
            {
                RegisteredUser = new RegisterForm(new List<Restaurant>(), Email, Name, pass: Pass, favNum: Num);
                RegisterService.SetAll(this.RegisteredUser.AddToList(RegisteredUsers));
                RegistrationEventNotifier var = new(Email);
                return RedirectToPage("/Forms/LoginScreen");
            }
        }
        public void RaiseEvent(string mail)
        {
            OnSucessfullRegistrationEvent?.Invoke(this, mail);
        }
    }
}

