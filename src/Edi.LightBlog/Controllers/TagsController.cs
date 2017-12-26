using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    public class TagsController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public TagsController(ILogger<TagsController> logger,
            IOptions<AppSettings> settings,
            IConfiguration configuration,
            IHttpContextAccessor accessor)
            : base(logger, settings, configuration)
        {
            _accessor = accessor;
            TagRepository = new TagRepository();
        }

        public TagRepository TagRepository { get; set; }

        public IActionResult Index()
        {
            var tags = TagRepository.Read();
            return View(tags);
        }

        [Route("tags/list/{normalizedName}")]
        public IActionResult List(string normalizedName)
        {
            return View();
        }

        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }
    }
}