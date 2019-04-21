using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeachHacks.DAL;
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

        static async Task GetPresidentialCandidateTweets(TwitterContext twitterCtx)
        {
            //List<Status> tweets =
            //    await
            //    (from tweet in twitterCtx.Status
            //     where tweet.Type == StatusType.User &&
            //           tweet.ScreenName == "JoeMayo"
            //     select tweet)
            //    .ToListAsync();

            const int MaxTweetsToReturn = 200;
            const int MaxTotalResults = 100;

            // oldest id you already have for this search term
            ulong sinceID = 1;

            // used after the first query to track current session
            ulong maxID;

            var combinedSearchResults = new List<Status>();

            List<Status> tweets =
                await
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.User &&
                       tweet.ScreenName == "JoeMayo" &&
                       tweet.Count == MaxTweetsToReturn &&
                       tweet.SinceID == sinceID &&
                       tweet.TweetMode == TweetMode.Extended
                 select tweet)
                .ToListAsync();

            if (tweets != null)
            {
                combinedSearchResults.AddRange(tweets);
                ulong previousMaxID = ulong.MaxValue;
                do
                {
                    using(PolitiFactContext db = new PolitiFactContext())
                    {

                    }
                    // one less than the newest id you've just queried
                    maxID = tweets.Min(status => status.StatusID) - 1;

                    Debug.Assert(maxID < previousMaxID);
                    previousMaxID = maxID;

                    tweets =
                        await
                        (from tweet in twitterCtx.Status
                         where tweet.Type == StatusType.User &&
                               tweet.ScreenName == "JoeMayo" &&
                               tweet.Count == MaxTweetsToReturn &&
                               tweet.MaxID == maxID &&
                               tweet.SinceID == sinceID &&
                               tweet.TweetMode == TweetMode.Extended
                         select tweet)
                        .ToListAsync();

                    combinedSearchResults.AddRange(tweets);

                } while (tweets.Any() && combinedSearchResults.Count < MaxTotalResults);

            }
            else
            {
                Console.WriteLine("No entries found.");
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
