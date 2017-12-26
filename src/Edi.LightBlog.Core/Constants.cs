namespace Edi.LightBlog.Core
{
    public class Constants
    {
        public static string AdminRoleName = "Administrators";

        public static string CookieAuthenticationSchemeName = "LightBlogCookieAuthenticationScheme";

        public static string DbConnectionName = "DatabaseConnection";
    }

    public enum AuthType
    {
        TOTP
    }
}
