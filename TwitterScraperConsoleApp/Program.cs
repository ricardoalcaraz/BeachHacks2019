using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeachHacks.DAL;
using BeachHacks.Models;
using LinqToTwitter;

namespace TwitterScraperConsoleApp
{
    class Program
    {
        private static string CONSUMER_API_KEY = "UByS3PqIBMP5wyvzASJQIzAGz";
        private static string CONSUMER_SECRET_KEY = "udUokmF4bPs86ebnagaGr0Yws8bi5Rii4Awvnv6Snv7wwoDNFI";

        static void Main(string[] args)
        {
            ShowMenu();

            IAuthorizer authorizer = DoPinOAuth();
            authorizer.AuthorizeAsync().Wait();
            var twitterCtx = new TwitterContext(authorizer);
            string twitterHandle = "AndrewYang";
            GetPresidentialCandidateTweets(twitterCtx, twitterHandle).Wait();
            //AndrewYang
            Console.Write("\nPress any key to close console window...");
            Console.ReadKey(true);
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
                GoToTwitterAuthorization = pageLink => Console.WriteLine(pageLink),
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

        static async Task GetPresidentialCandidateTweets(TwitterContext twitterCtx, string twitterHandle)
        {
            const int MaxTweetsToReturn = 3200;

            // oldest id you already have for this search term
            ulong sinceID = 1;

            var combinedSearchResults = new List<Status>();

            List<Status> tweets =
                await
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.User &&
                       tweet.ScreenName == twitterHandle &&
                       tweet.Count == MaxTweetsToReturn &&
                       tweet.SinceID == sinceID &&
                       tweet.TweetMode == TweetMode.Extended
                 select tweet)
                .ToListAsync();

            if (tweets != null && tweets.Any())
            {
                WriteTweetsToDatabase(tweets, twitterHandle);
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
        }

        static void WriteTweetsToDatabase(List<Status> tweets, string twitterHandle)
        {
            using (PolitiFactContext db = new PolitiFactContext())
            {
                foreach(var tweet in tweets)
                {
                    Tweet statusAsTweet = new Tweet
                    {
                        Text = tweet.Text,
                        TwitterUserId = (long)tweet.UserID,
                        TwitterName = twitterHandle,
                        PoliticalCandidate = 4
                    };
                    db.Tweet.Add(statusAsTweet);
                }
            }
        }

        static void PrintTweetsResults(List<Status> tweets)
        {
            if (tweets != null)
                tweets.ForEach(tweet =>
                {
                    if (tweet != null && tweet.User != null)
                        Console.WriteLine(
                            "Name: {0}, Tweet: {1}",
                            tweet.User.ScreenNameResponse, tweet.Text);
                });
        }

    }
}
