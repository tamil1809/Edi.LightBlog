using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string AuthorName { get; set; }

        public string ContentAbstract { get; set; }

        public string HtmlContent { get; set; }

        public bool CommentEnabled { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public int Hits { get; set; }
    }

    public class PostEditModel : Post
    {
        public List<Tag> Tags { get; set; }


    }

    public class PostReadViewModel
    {
        public Post Post { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Category> Categories { get; set; }

        public List<Comment> Comments { get; set; }

        public PostReadViewModel()
        {
            Tags = new List<Tag>();
            Categories = new List<Category>();
            Comments = new List<Comment>();
        }
    }
}
