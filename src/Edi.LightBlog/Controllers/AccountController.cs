using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwoStepsAuthenticator;

namespace Edi.LightBlog.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {
        public AccountController(ILogger<AccountController> logger,
            IOptions<AppSettings> settings)
            : base(logger, settings)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignIn(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(Constants.CookieAuthenticationSchemeName);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var b = PerformTOTPAuthentication(model.TotpCode);
#if DEBUG
                b = true;
#endif
                if (b)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, AppSettings.AdminName),
                        new Claim(ClaimTypes.Role,"Administrators")
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims,
                        Constants.CookieAuthenticationSchemeName);
                    var principal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(
                        Constants.CookieAuthenticationSchemeName, 
                        principal, 
                        new AuthenticationProperties
                        {
                            IsPersistent = false
                        });

                    //HttpContext.User = principal;
                    //var tokens = _antiforgery.GetTokens(HttpContext);
                    //_antiforgery.GetAndStoreTokens(HttpContext);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool PerformTOTPAuthentication(string totpCode)
        {
            var secret = AppSettings.TOTPSecret;
            var authenticator = new TimeAuthenticator();
            var ok = authenticator.CheckCode(secret, totpCode, null);
            return ok;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(Constants.CookieAuthenticationSchemeName);
            return RedirectToAction("SignIn");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }
    }
}