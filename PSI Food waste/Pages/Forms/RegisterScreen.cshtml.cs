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
    public class RegisterScreenModel : PageModel
    {
        public RegisteredUser<RegisterForm> RegisteredUsers { get; set; }/* = new RegisteredUser<RegisterForm>();*/

        public RegisterForm RegisteredUser { get; set; }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Pass { get; set; }
        [BindProperty]
        public int num { get; set; }
        [BindProperty]
        public string Msg { get; set; }

        public void OnGet()
        {
            RegisteredUsers = RegisterService.GetAll();
            //if(RegisteredUsers == null)
            //{
                //RegisteredUsers = new RegisteredUser<RegisterForm>();
            //}
        }

        public void OnPost()
        {
            RegisteredUser = new RegisterForm(Name, Pass, 2);
            RegisterService.AddToList(RegisteredUser);
            RegisteredUsers = RegisterService.GetAll();
        }
    }
}
