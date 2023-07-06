using System;
namespace Infrastructure.Services
{
    public class AuthSenderOptions
    {
        private readonly string user = "Toy Swap Hub"; // The name you want to show up on your email
                                                        // Make sure the string passed in below matches your API Key
        public AuthSenderOptions()
        {
            key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        }
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }

    }
}
