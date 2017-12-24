using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.TwoStepsAuthenticator;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
                if (b)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("name", AppSettings.AdminName),
                        new Claim("role","Administrators")
                    };

                    var ci = new ClaimsIdentity(claims, AuthType.TOTP.ToString(), "name", "role");
                    var p = new ClaimsPrincipal(ci);

                    await HttpContext.SignInAsync(Constants.CookieAuthenticationSchemeName, p);
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