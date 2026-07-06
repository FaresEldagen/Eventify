using Eventify.Validators;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        //public int? Role { get; set; }

       // only for validation
        public string? LoginError { get; set; }

    }
}


