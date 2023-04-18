using System;
namespace Infrastructure.Services
{
    public class AuthSenderOptions
    {
        private readonly string user = "Toy Swap Hub"; // The name you want to show up on your email
                                                        // Make sure the string passed in below matches your API Key
        private readonly string key = "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";
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }

    }
}