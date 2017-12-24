using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Core;
using Edi.LightBlog.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Edi.LightBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Edi.LightBlog.Controllers
{
    public class PostController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public PostController(ILogger<PostController> logger,
            IOptions<AppSettings> settings,
            IConfiguration configuration,
            IHttpContextAccessor accessor)
            : base(logger, settings, configuration)
        {
            _accessor = accessor;
            PostRepository = new PostRepository();
            PostTagAssociationRepository = new PostTagAssociationRepository();
            PostCategoryAssociationRepository = new PostCategoryAssociationRepository();
            CommentRepository = new CommentRepository();
        }

        public PostRepository PostRepository { get; set; }

        public PostTagAssociationRepository PostTagAssociationRepository { get; set; }

        public PostCategoryAssociationRepository PostCategoryAssociationRepository { get; set; }

        public CommentRepository CommentRepository { get; set; }

        public IActionResult Index()
        {
            var posts = PostRepository.Read();
            return View(posts);
        }

        [Route("posts/{slug}")]
        public IActionResult Read(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var strSlug = slug.Trim().ToLower();
            var post = PostRepository.GetPostBySlug(strSlug);
            if (null != post)
            {
                // get tags
                var tags = PostTagAssociationRepository.GetTagsByPost(post.Id);

                // get categories
                // var cats = PostCategoryAssociationRepository

                // get comments
                var cmts = CommentRepository.GetCommentsByPostId(post.Id);

                var viewModel = new PostReadViewModel()
                {
                    Post = post,
                    Tags = tags?.ToList() ?? new List<Tag>(),
                    Comments = cmts?.ToList() ?? new List<Comment>()
                };
                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
