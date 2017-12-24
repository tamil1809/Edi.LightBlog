using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    public class CategoryController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public CategoryController(ILogger<CategoryController> logger,
            IOptions<AppSettings> settings,
            IConfiguration configuration,
            IHttpContextAccessor accessor)
            : base(logger, settings, configuration)
        {
            _accessor = accessor;
            CategoryRepository = new CategoryRepository();
        }

        public CategoryRepository CategoryRepository { get; set; }


        public IActionResult Index()
        {
            var cats = CategoryRepository.Read();
            return View(cats);
        }

        [Route("category/{routeName}")]
        public IActionResult List(string routeName)
        {
            return View();
        }
    }
}