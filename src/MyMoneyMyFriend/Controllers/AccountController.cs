using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Entities;
using MyMoneyMyFriend.ViewModels;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Controllers
{
    public class AccountController : Controller
    {
        /*
         Note that default endpoint for user logins is at Account/login. 
         For that to work we need to have a controller called Account, so that requests are handled.
         */

        private SignInManager<User> _signInmanager;
        private UserManager<User> _userManager;
        /* Some identity framework services:
        UserManager: Customized with type of my user.  This service allows us to create user and etc.
            We could directly go to dbContext class and access the asp.net users table. 
            But we want to use the user manager to be sure that the  user is created in a safe manner.
        SignInManager: Allows use to sign in and sign out a user, it takes care o issuing the cookies.
        */
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInmanager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // The function to handle postings back to Register end point
        /* 
         The input of the Register action should not be User. Because someone may give more information that is expected. 
         The RegisterUserViewModel has the necessary fields that the user has to fill in, nothing more nothing less. 
         */
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // if state is valid create the user. 
                var user = new User { UserName = model.Username };
                /*
                 This is async method. For async methods the return type is wrapped in a task.
                 The await keyword, waits for the result of another async method.
                 */
                var createResult = await _userManager.CreateAsync(user, model.Password);
                if (createResult.Succeeded)
                {
                    // When isPersistant set to true: Sign in manager will issue a cookie to browser. 
                    // That cookies will an authentication ticket that browser sends back on every subsequent request. 
                    // The cookie is persistent meaning will not go away when the user does not closes the browser. 
                    // When isPersistant set to false: Will create a session cookie and therefore goes away when the user closes the browser. 
                    await _signInmanager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        /* ModelState.AddModelError("Username", error.Description);
                         * The above will associate the error with a specific property (in this case Username) on the model for the view.
                         * And therefore it will show up in the validation that is associated with the thatt property (Username in this case)
                         */
                        // Bellow, we don't want the error to be associated with a specific key. So the key is blank
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            //if not valid state, return the same register view 
            return View();
        }

        // Note that all of identity framework methods are asynchronous 
        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            // The bellow will remove the authentication cookie from users browser.
            await _signInmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Responds to get requests and present the user with login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // Responds to post requests, validates the form inputs and potentially signs in the user first
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // The var below is of type Microsoft.AspNetCore.Identity.SignInResult
                var loginResult = await _signInmanager.PasswordSignInAsync(model.Username, model.Password,
                    model.RememberMe, false);
                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        // This check is required to make sure you don't have a security problem in the application with open redirects. This helps prevent open redirects
                        // If you have the url, you can use Redirect instead of RedirectToAction
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Note that LoginResult (unlike CreateResult) does not have errors collection. 
                // Instead it has some flags about if the user is locked out or is not allowed, confirming emails etc
            }
            // Bellow could appear, if the user enters wrong user name or wrong password.  Remember this is not associated to any property.
            ModelState.AddModelError("", "Could not login");
            // if could not succeed, it will return the login view again with the above login error 
            return View();
        }
    }
}
