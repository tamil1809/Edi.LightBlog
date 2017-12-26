using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        public string TagName { get; set; }

        public string NormalizedName { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
