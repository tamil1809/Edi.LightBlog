using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Edi.LightBlog.Models
{
    public class CategoryEditModel
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.Text)]
        public string Title { get; set; }

        [Required, DataType(DataType.Text)]
        public string Description { get; set; }

        [Required, DataType(DataType.Text)]
        [RegularExpression("/^[a-zA-Z]+$/")]
        public string RouteName { get; set; }
    }
}
