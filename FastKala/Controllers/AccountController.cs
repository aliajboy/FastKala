using FastKala.Application.Data;
using FastKala.Application.ViewModels.Global;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace FastKala.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<FastKalaUser> _userManager;
    private readonly SignInManager<FastKalaUser> _signInManager;
    private readonly IEmailSender _emailSender;

    public AccountController(SignInManager<FastKalaUser> signInManager, UserManager<FastKalaUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    [Route("Login")]
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        if (!_signInManager.IsSignedIn(User))
        {
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        return LocalRedirect("/");
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
    public IActionResult Register(string returnUrl = null)
    {
        if (!_signInManager.IsSignedIn(User))
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        return LocalRedirect("/");
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(LoginViewModel loginViewModel, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            if (!loginViewModel.RememberMe)
            {
                ModelState.AddModelError(string.Empty, "برای ثبت نام نیاز است که قوانین سایت را تایید کنید!");
                return View();
            }

            var user = new FastKalaUser
            {
                UserName = loginViewModel.Email,
                Email = loginViewModel.Email
            };
            var result = await _userManager.CreateAsync(user, loginViewModel.Password);
            if (result.Succeeded)
            {
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    // Confirm Email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(loginViewModel.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return RedirectToPage("RegisterConfirmation", new { email = loginViewModel.Email, returnUrl = returnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return View();
    }
}
