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

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        #region SugnUp

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



        #region SugnIn

        #endregion



        #region SugnOut

        #endregion
    }
}
