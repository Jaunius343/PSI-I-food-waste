using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSI_Food_waste.Models;
using PSI_Food_waste.Services;

namespace PSI_Food_waste.Pages.Forms
{
    public class RegisterScreenModel : PageModel
    {
        public RegisteredUser<RegisterForm> RegisteredUsers { get; set; }/* = new RegisteredUser<RegisterForm>();*/

        public RegisterForm RegisteredUser { get; set; }

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
            RegisteredUsers = RegisterService.GetAll();
            //if(RegisteredUsers == null)
            //{
            //RegisteredUsers = new RegisteredUser<RegisterForm>();
            //}
        }
        public IActionResult OnPost()
        {
            // Username validation
            Regex regex = new Regex(@"^\w{3,20}$");

            // password validation
            Regex regex2 = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\@$!%*#?&])[A-Za-z\d\@$!%*#?&]{8,}$");

            RegisteredUsers = RegisterService.GetAll();
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
                RegisteredUser = new RegisterForm(Name, pass : Pass, favNum : Num);
                //this.RegisteredUser.AddToList();
                RegisterService.SetAll(this.RegisteredUser.AddToList(RegisteredUsers));
                //RegisterService.AddToList(RegisteredUser);
                ErrorMsg = "";
                return Page();
            }
        }
    }
}
