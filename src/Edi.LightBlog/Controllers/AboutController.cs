using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.Data;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    public class AboutController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public AboutController(ILogger<AboutController> logger,
            IOptions<AppSettings> settings,
            IConfiguration configuration,
            IHttpContextAccessor accessor)
            : base(logger, settings, configuration)
        {
            _accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            var mdFilePath = Path.Combine(dataDir, "About.md");
            if (System.IO.File.Exists(mdFilePath))
            {
                _logger.LogInformation($"Reading file: {mdFilePath}");

                var mdContent = await System.IO.File.ReadAllTextAsync(mdFilePath);
                var htmlContent = CommonMark.CommonMarkConverter.Convert(mdContent);
                var model = new AboutModel() { PageContent = htmlContent };
                return View(model);
            }
            return NotFound();
        }
    }
}