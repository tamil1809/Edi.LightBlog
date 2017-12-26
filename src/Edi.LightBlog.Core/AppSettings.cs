using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edi.LightBlog.Core
{
    public class AppSettings
    {
        public string Title { get; set; }

        public string Copyright { get; set; }

        public string AdminName { get; set; }

        public string TOTPSecret { get; set; }

        public float TimeZone { get; set; }

        public string MetaKeyword { get; set; }
    }
}
