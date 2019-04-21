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
            string twitterHandle = "SenWarren";
            int presidentialCandidateID = 1;
            GetPresidentialCandidateTweets(twitterCtx, twitterHandle, presidentialCandidateID).Wait();
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

        static async Task GetPresidentialCandidateTweets(TwitterContext twitterCtx, string twitterHandle, int presidentialCandidateID)
        {
            const int MaxTweetsToReturn = 100;
            const int MaxTotalResults = 3200;
            // oldest id you already have for this search term
            ulong sinceID = 1;
            ulong maxID;
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

            if (tweets != null)
            {
                combinedSearchResults.AddRange(tweets);
                ulong previousMaxID = ulong.MaxValue;

                do
                {
                    // one less than the newest id you've just queried
                    maxID = tweets.Min(status => status.StatusID) - 1;

                    Debug.Assert(maxID < previousMaxID);
                    previousMaxID = maxID;

                    tweets =
                        await
                        (from tweet in twitterCtx.Status
                         where tweet.Type == StatusType.User &&
                               tweet.ScreenName == twitterHandle &&
                               tweet.Count == MaxTweetsToReturn &&
                               tweet.MaxID == maxID &&
                               tweet.SinceID == sinceID &&
                               tweet.TweetMode == TweetMode.Extended
                         select tweet)
                        .ToListAsync();

                    combinedSearchResults.AddRange(tweets);

                } while (tweets.Any() && combinedSearchResults.Count < MaxTotalResults);

                WriteTweetsToDatabase(combinedSearchResults, twitterHandle, presidentialCandidateID);
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
        }
        /*
         *  List<Status> tweets =
                await
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.User &&
                       tweet.ScreenName == "JoeMayo" &&
                       tweet.ScreenName == twitterHandle &&
                       tweet.Count == MaxTweetsToReturn &&
                       tweet.SinceID == sinceID &&
                       tweet.TweetMode == TweetMode.Extended
                 select tweet)
                .ToListAsync();
 
            if (tweets != null)
            if (tweets != null && tweets.Any())
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
          */
        static void WriteTweetsToDatabase(List<Status> tweets, string twitterHandle, int presidentialCandidateID)
        {
            using (PolitiFactContext db = new PolitiFactContext())
            {
                foreach(var tweet in tweets)
                {
                    long userID = 0;
                    Int64.TryParse(tweet.User.UserIDResponse, out userID);
                    Tweet statusAsTweet = new Tweet
                    {
                        Text = tweet?.FullText ?? "",
                        TwitterUserId = userID,
                        TwitterName = twitterHandle,
                        PoliticalCandidate = presidentialCandidateID,
                        Time = tweet.CreatedAt
                    };
                    db.Tweet.Add(statusAsTweet);
                }
                db.SaveChanges();
                Console.Write("Saved a total of " + tweets.Count + " tweets to the database for " + twitterHandle);
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
