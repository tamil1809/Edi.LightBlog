using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AdminController : ControllerBase
    {
        public AdminController(ILogger<AccountController> logger,
            IOptions<AppSettings> settings)
            : base(logger, settings)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateConfiguration(AppSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Configuration Updated.";
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }
    }
}