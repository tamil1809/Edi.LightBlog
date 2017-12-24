using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PlainContent { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public int PostId { get; set; }
    }
}
