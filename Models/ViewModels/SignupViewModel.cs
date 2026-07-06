using Eventify.Validators;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels
{
    public class SignupViewModel  
    {
        [CheckEmailUnique]
        [EmailAddress(ErrorMessage = "Invalid format")]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [CheckUsernameUnique]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-Z][a-zA-Z ]*$", ErrorMessage = "Invalide format: only alphabets and spaces")]
        public string Username { get; set; }


        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{6,}$",
    ErrorMessage = "Invalid Password: must be chars, with upper, lower, number & special char.")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }



        [Compare("Password", ErrorMessage = "Password not match")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }


        //[Require//
        //[Range(1//)]
        public int? Role { get; set; }
    }
}
