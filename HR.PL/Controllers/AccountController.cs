using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace HR.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;   
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
		#region Register
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)//Server Side Validation
			{
				var User = new ApplicationUser()
				{
					Email = model.Email,
					UserName = model.Email.Split('@')[0],// The Name Before @
					FirstName = model.FName,
					LastName = model.LName,
					IsAgree = model.IsAgree
				};
				var Result = await _userManager.CreateAsync(User, model.Password);

				if (Result.Succeeded)

					return RedirectToAction(nameof(Login));
				foreach (var error in Result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

			}
			return View(model);
		}

		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)//Server Side Validation
			{
				var User = await _userManager.FindByEmailAsync(model.Email);

				if (User is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if (flag)
					{
						var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded) return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError(string.Empty, "Password Is Invalid");

				}
				ModelState.AddModelError(string.Empty, "Email Is Invalid");

			}
			return View(model);
		}

		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion
	}
}

