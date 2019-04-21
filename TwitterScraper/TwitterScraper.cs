using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TwitterScraper
{
    public class TwitterScraper
    {


        public TwitterScraper()
        {

            //@SenWarren
            //@BernieSanders
            //Get candidate data and write it to the database
        }

        public void GetCandidateTweets()
        {

        }
        /*
        static async Task RunUserTimelineQueryAsync(TwitterContext twitterCtx)
        {
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
                       tweet.ScreenName == "SenWarren" &&
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
                               tweet.ScreenName == "JoeMayo" &&
                               tweet.Count == MaxTweetsToReturn &&
                               tweet.MaxID == maxID &&
                               tweet.SinceID == sinceID &&
                               tweet.TweetMode == TweetMode.Extended
                         select tweet)
                        .ToListAsync();

                    combinedSearchResults.AddRange(tweets);

                } while (tweets.Any() && combinedSearchResults.Count < MaxTotalResults);
                Console.WriteLine(tweets);
                //PrintTweetsResults(tweets);
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
        }
        */
    }
}
