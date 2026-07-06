using Eventify.Models.Entities;
using Eventify.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventify.Models.Enums;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> _signIn;

        public AccountController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signIn)
        {
            _user = user;
            _signIn = signIn;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _user.FindByEmailAsync(loginViewModel.Email!);

            if (user == null)
            {
                ModelState.AddModelError("LoginError", "• Invalid Email or Password");
                return View(loginViewModel);
            }

            var loginResult = await _signIn.PasswordSignInAsync(user, loginViewModel.Password!,false,false);
            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("LoginError", "• Invalid Email or Password");
                return View(loginViewModel);
            }

            return RedirectToAction("Index","Home");
        }

        public IActionResult SignUp()
        
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignupViewModel signupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signupViewModel);
            }
            return View("SignUp2", signupViewModel);
        }

        public  IActionResult SignUp2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp2(SignupViewModel signupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signupViewModel);
            }
            ApplicationUser? newUser = null;
            if(signupViewModel.Role == 1)
            {
                newUser = new Owner
                {
                    UserName = signupViewModel.Username,
                    Email = signupViewModel.Email,
                    AccountStatus = AccountStatus.NotVerified
                };
            }
            else if(signupViewModel.Role == 2)
            {
                newUser = new Organizer
                {
                    UserName = signupViewModel.Username,
                    Email = signupViewModel.Email,
                    AccountStatus = AccountStatus.NotVerified
                };
            }

            var registerResult = await _user.CreateAsync(newUser!, signupViewModel.Password);
            if (!registerResult.Succeeded)
            {
                foreach (var item in registerResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            else
            {
                var roleResult = await _user.AddToRoleAsync(newUser!, (signupViewModel.Role == 1) ? "Owner" : "Organizer");
                return RedirectToAction("Login", "Account");
            }
            return View(signupViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

    }
}
