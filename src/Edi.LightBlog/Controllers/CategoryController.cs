﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.Data;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    [AutoValidateAntiforgeryToken]
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
            PostCategoryAssociationRepository = new PostCategoryAssociationRepository();
        }

        public CategoryRepository CategoryRepository { get; set; }

        public PostCategoryAssociationRepository PostCategoryAssociationRepository { get; set; }

        public IActionResult Index()
        {
            var cats = CategoryRepository.Read();
            return View(cats);
        }

        [Route("category/list/{routeName}")]
        public IActionResult List(string routeName)
        {
            return View();
        }

        #region Management

        [Authorize]
        public IActionResult Manage()
        {
            var cats = CategoryRepository.Read();
            return View(cats);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize, HttpPost]
        public IActionResult Create(CategoryEditModel model)
        {
            if (ModelState.IsValid)
            {
                var cat = new Category()
                {
                    Title = model.Title,
                    RouteName = model.RouteName,
                    Description = model.Description
                };

                var rows = CategoryRepository.Create(cat);
                if (rows > 0)
                {
                    return RedirectToAction(nameof(Manage));
                }
                ModelState.AddModelError(string.Empty, "Create Category Failed on Data Layer.");
            }
            return View();
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var cat = CategoryRepository.Read(id);
            if (null != cat)
            {
                var catEdit = new CategoryEditModel()
                {
                    Id = cat.Id,
                    Description = cat.Description,
                    RouteName = cat.RouteName,
                    Title = cat.Title
                };
                return View(catEdit);
            }
            return NotFound();
        }

        [Authorize, HttpPost]
        public IActionResult Edit(CategoryEditModel model)
        {
            if (ModelState.IsValid)
            {
                var c = CategoryRepository.Read(model.Id);
                if (null == c)
                {
                    return NotFound();
                }

                var cat = new Category()
                {
                    Id = model.Id,
                    Title = model.Title,
                    RouteName = model.RouteName,
                    Description = model.Description
                };

                var rows = CategoryRepository.Update(cat);
                if (rows > 0)
                {
                    return RedirectToAction(nameof(Manage));
                }
                ModelState.AddModelError(string.Empty, "Create Category Failed on Data Layer.");
            }
            return View();
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var cat = CategoryRepository.Read(id);
            if (null != cat)
            {
                return View(cat);
            }
            return NotFound();
        }

        [Authorize, HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var cat = CategoryRepository.Read(id);
            if (null != cat)
            {
                // 1. Delete Associations
                PostCategoryAssociationRepository.DeleteByCategoryId(id);

                // 2. Delete Category itself
                CategoryRepository.Delete(id);
                return RedirectToAction(nameof(Manage));
            }
            return NotFound();
        }

        #endregion
    }
}