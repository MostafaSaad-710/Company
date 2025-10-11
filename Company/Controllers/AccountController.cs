using Company.DAL.Models;
using Company.Dots;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            

            if (ModelState.IsValid) //Server Side Validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        //Register


                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            // Send Email To Confirm Email
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }

                    }

                }

                ModelState.AddModelError("", "InValid Sign Up !!");
            }

           

            return View(model);
        }

        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
               var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if(flag)
                    {
                        // Sign In
                        var result =await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }
                    }
                }

                ModelState.AddModelError("", "Invalid Login !!");
            }

            return View(model);
        }



        #endregion

        #region SugnOut

        #endregion

        #region Forget Password

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendResetPasswordUrl( ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    // Denf 
                }

            }
            ModelState.AddModelError("", "Invalid Reset Password Operation !!");
            return View("ForgetPassword" , model);
        }

        #endregion

    }
}
