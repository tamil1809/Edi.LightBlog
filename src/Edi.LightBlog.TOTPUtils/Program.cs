using System;
using TwoStepsAuthenticator;

namespace Edi.LightBlog.TOTPUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            var newSecret = Authenticator.GenerateKey(6);
            Console.WriteLine(newSecret);
            Console.ReadLine();
        }
    }
}
