using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class SignInModel
    {
        [Required]
        public string TotpCode { get; set; }
    }
}
