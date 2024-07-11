using FastKala.Application.Data;
using FastKala.Application.ViewModels.Global;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<FastKalaUser> _userManager;
    private readonly SignInManager<FastKalaUser> _signInManager;

    public string ReturnUrl { get; set; }

    public AccountController(SignInManager<FastKalaUser> signInManager, UserManager<FastKalaUser> userManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("Login")]
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ReturnUrl = returnUrl;
        return View();
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است!");
                return View();
            }
        }

        // If we got this far, something failed, redisplay form
        return View();
    }

    [Route("Register")]
    [HttpGet]
    public async Task<IActionResult> Register(string returnUrl = null)
    {
        return View();
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(LoginViewModel loginViewModel, string returnUrl = null)
    {
        return View();
    }
}
