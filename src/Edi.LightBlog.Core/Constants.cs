namespace Edi.LightBlog.Core
{
    public class Constants
    {
        public static string AdminRoleName = "Administrators";

        public static string DbConnectionName = "DatabaseConnection";

        public static string CookieAuthenticationSchemeName = "EdiLightBlogCookieAuthenticationScheme";
    }

    public enum AuthType
    {
        TOTP
    }
}
