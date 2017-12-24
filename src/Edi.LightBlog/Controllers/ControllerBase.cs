using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Edi.LightBlog.Core;
using Edi.LightBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace Edi.LightBlog.Controllers
{
    public class ControllerBase : Controller
    {
        public IConfiguration Configuration { get; }

        protected readonly ILogger<ControllerBase> _logger;

        protected AppSettings AppSettings { get; set; }

        public Func<IActionResult> BlowUpDefaultFunction { get; protected set; }

        public ControllerBase(ILogger<ControllerBase> logger = null,
                              IOptions<AppSettings> settings = null,
                              IConfiguration configuration = null)
        {
            BlowUpDefaultFunction = () => new StatusCodeResult(500);

            if (null != logger)
            {
                _logger = logger;
            }

            if (null != settings)
            {
                AppSettings = settings.Value;
            }

            if (null != configuration)
            {
                Configuration = configuration;
            }
        }

        public IActionResult ServerBlowUp()
        {
            return new StatusCodeResult(500);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TryResponse(Func<IActionResult> execFunc,
            Func<IActionResult> blowUpFunc = null)
        {
            try
            {
                return execFunc();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return blowUpFunc != null ? blowUpFunc() : BlowUpDefaultFunction();
            }
        }

        public async Task<IActionResult> TryResponseAsync(Func<Task<IActionResult>> execFunc,
            Func<IActionResult> blowUpFunc = null)
        {
            try
            {
                return await execFunc();
            }
            catch (Exception)
            {
                if (blowUpFunc != null)
                {
                    return blowUpFunc();
                }
                throw;
            }
        }
    }
}