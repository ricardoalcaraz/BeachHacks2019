using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TwitterScraper
{
    public class Program
    {
        private static string CONSUMER_API_KEY = "UByS3PqIBMP5wyvzASJQIzAGz";
        private static string CONSUMER_SECRET_KEY = "udUokmF4bPs86ebnagaGr0Yws8bi5Rii4Awvnv6Snv7wwoDNFI";

        public static void Main()
        {
            ShowMenu();

            IAuthorizer authorizer = DoPinOAuth();
            var twitterCtx = new TwitterContext(authorizer);
        }

        static void ShowMenu()
        {
            Console.WriteLine("Twitter Scraper");
            Console.WriteLine("Please enter your pin");
        }

        static IAuthorizer DoPinOAuth()
        {
            var auth = new PinAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = CONSUMER_API_KEY,
                    ConsumerSecret = CONSUMER_SECRET_KEY
                },
                GoToTwitterAuthorization = pageLink => Process.Start(pageLink),
                GetPin = () =>
                {
                    Console.WriteLine(
                        "\nAfter authorizing this application, Twitter " +
                        "will give you a 7-digit PIN Number.\n");
                    Console.Write("Enter the PIN number here: ");
                    return Console.ReadLine();
                }
            };

            return auth;
        }
    }
}

