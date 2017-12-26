using System;
using System.Collections.Generic;
using System.Text;

namespace Edi.LightBlog.Core
{
    public interface IAppInfomation
    {
        string GetAppVersion();
    }

    public class AppInfomation : IAppInfomation
    {
        public string GetAppVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}
