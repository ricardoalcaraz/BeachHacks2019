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
        private const int DAYS_BACK = -10;

        /*
         *        
         */
        [HttpGet("{handle}")]
        public ActionResult<IEnumerable<string>> Get(string handle)
        {
            try
            {
                using (PolitiFactContext db = new PolitiFactContext())
                {
                    // Query tweets given amount of days back
                    DateTime dt = DateTime.Now.AddDays(DAYS_BACK);
                    var query = db.Tweet.Where(a => a.TwitterName == handle && a.Time >= dt).ToList();

                    List<List<EntityDTO>> analyses = new List<List<EntityDTO>>();
                    AnalyticsEngine ae = new AnalyticsEngine();


                    // Analyze per Tweet
                    foreach (Tweet t in query)
                    {
                        var client = LanguageServiceClient.Create();
                        var eresponse = client.AnalyzeEntitySentiment(new Document()
                        {
                            Content = t.Text,
                            Type = Document.Types.Type.PlainText
                        });

                        analyses.Add(ae.getAnalysis(eresponse.Entities));

                        //if (ae.isTokenSufficient(t.Text))
                        //{
                        //    var cresponse = client.ClassifyText(new Document()
                        //    {
                        //        Content = t.Text,
                        //        Type = Document.Types.Type.PlainText
                        //    });

                        //    analyses.Add(ae.getAnalysis(eresponse.Entities, cresponse.Categories));
                        //}
                        //else
                        //{
                        //    analyses.Add(ae.getAnalysis(eresponse.Entities, Enumerable.Empty<ClassificationCategory>()));
                        //}



                        WriteDataToDatabase(db, analyses.Last(), t);
                    }

                    return Ok(analyses);
                }
            } catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //The following process is gonna be really fucking slow so never repeat this shit
        private void WriteDataToDatabase(PolitiFactContext db, List<EntityDTO> analysis, Tweet tweet)
        {
            try
            {
                foreach(var entry in analysis)
                {
                        var entityEntry = new Entities
                        {
                            TweetId = tweet.TweetId,
                            Salience = (decimal)entry.Salience,
                            Type = entry.Type.ToString()
                        };
                        db.Entities.Add(entityEntry);
                }
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
