using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Authentication_Lab.Domain;

namespace Authentication_Lab.Pages.Admin
{
    public class RegisterModel : PageModel
    {
        private User User = new User();
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Role { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            User.Username = Username;
            User.Email = Email;
            User.Password = Password;
            User.Role = Role;

        }
    }
}
