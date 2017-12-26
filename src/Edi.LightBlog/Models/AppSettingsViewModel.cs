using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class AppSettingsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Copyright { get; set; }

        [Required]
        public string AdminName { get; set; }

        [Required]
        [RegularExpression("([a-zA-Z])\\w+")]
        [MinLength(6), MaxLength(8)]
        public string TOTPSecret { get; set; }

        [Required]
        public float TimeZone { get; set; }

        [Required]
        public string MetaKeyword { get; set; }
    }
}
