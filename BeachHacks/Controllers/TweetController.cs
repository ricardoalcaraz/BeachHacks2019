using System.Collections.Generic;
using System;
using System.Linq;
using BeachHacks.DAL;
using BeachHacks.DTO;
using BeachHacks.Models;
using BeachHacks.Services;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Language.V1;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeachHacks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : Controller
    {
        // private const int DAYS_BACK = -10;
        private readonly PolitiFactContext _context;
        /*
         *        
         */
        public TweetController(PolitiFactContext context)
        {
            _context = context;
        }
        [HttpGet("{handle}")]
        public ActionResult<IEnumerable<string>> Get(string handle)
        {
            try
            {
                    // Query tweets given amount of days back
                    var query = (from entity in _context.Entities
                                join tweet in _context.Tweet on entity.TweetId equals tweet.TweetId
                                where tweet.TwitterName == handle
                                select new { entity, tweet })
                                .ToList();

                    List<List<object>> data = new List<List<object>>();

                    data.Add(new List<object> { "Date", "Mag", "Score" });

                    foreach (var a in query)
                    {
                        data.Add(new List<object> { a.tweet.Time.Value.Date, a.entity.SentimentMag, a.entity.SentimentScore });
                    }

                    return Ok(data);
            } catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //The following process is gonna be really fucking slow so never repeat this shit
        private void WriteDataToDatabase(List<EntityDTO> analysis, Tweet tweet)
        {
            try
            {
                foreach(var entry in analysis)
                {
                    if(!_context.Entities.Any(a => a.TweetId == tweet.TweetId))
                    {
                        var entityEntry = new Entities
                        {
                            TweetId = tweet.TweetId,
                            Salience = (decimal)entry.Salience,
                            SentimentMag = entry.Sentiment_Mag,
                            SentimentScore = entry.Sentiment_Score,
                            Type = entry.Type.ToString()
                        };
                        _context.Entities.Add(entityEntry);
                    }
                }
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
