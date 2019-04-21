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
    public class TestController : Controller
    {
        private const int DAYS_BACK = -10;

        // GET: /<controller>/


        //{
            //TestDTO response;
            //using (PolitiFactContext db = new PolitiFactContext())
            //{
            //    var democrat = db.Politicalparty.First();
            //    var presidentialCandidate = db.Presidentialcandidate.First();

            //    response = new TestDTO { Party = democrat.PartyName, Candidate = presidentialCandidate.Name };
            //}

            //return Ok(response);

            //TestDTO data;
            //var client = LanguageServiceClient.Create();
            //using (PolitiFactContext db = new PolitiFactContext())
            //{
            //    string post = db.Tweet.First().Text;
            //    var response = client.AnalyzeSentiment(new Document()
            //    {
            //        Content = post,
            //        Type = Document.Types.Type.PlainText
            //    });

            //    var sentiment = response.DocumentSentiment;

            //    data = new TestDTO { Content = post, Score = sentiment.Score , Magnitude = sentiment.Magnitude };
            //}

            //return Ok(data);
        //}
        /*
        [HttpGet("{handle}")]
        public ActionResult<IEnumerable<string>> Get(string handle)
        {
            //var client = LanguageServiceClient.Create();
               //using (PolitiFactContext db = new PolitiFactContext())
                //{
                //DateTime dt = new DateTime(2019, 4, 1);
                //    var query = db.Tweet.Where(a => a.TwitterName == handle && a.Time >= dt);

                //List<Tweet> tweets = new List<Tweet>();
                //    foreach (Tweet t in query)
                //    {
                //        tweets.Add(t);
                //    }

                //    return Ok(tweets);
                //}


            using (PolitiFactContext db = new PolitiFactContext())
            {
                DateTime dt = DateTime.Now.AddDays(DAYS_BACK);
                var query = db.Tweet
                    .Where(a => a.TwitterName == handle && a.Time >= dt)
                    .ToList();

                //List<List<EntityDTO>> data = new List<List<EntityDTO>>();

                //AnalysisDTO analysis = new AnalysisDTO();
                List<AnalysisDTO> analyses = new List<AnalysisDTO>();
                AnalyticsEngine ae = new AnalyticsEngine();

                foreach (Tweet t in query) {
                    var client = LanguageServiceClient.Create();
                    var eresponse = client.AnalyzeEntities(new Document()
                    {
                        Content = t.Text,
                        Type = Document.Types.Type.PlainText
                    });

                    if (ae.isTokenSufficient(t.Text))
                    {
                        var cresponse = client.ClassifyText(new Document()
                        {
                            Content = t.Text,
                            Type = Document.Types.Type.PlainText
                        });

                        analyses.Add(ae.getAnalysis(eresponse.Entities, cresponse.Categories));
                    }
                    else
                    {
                        analyses.Add(ae.getAnalysis(eresponse.Entities, Enumerable.Empty<ClassificationCategory>()));
                    }

                }

                return Ok(analyses);
            }

        }

    */
        //[HttpGet("{handle}")]
        //public ActionResult<IEnumerable<string>> Get(string handle)
        //{
        //    var client = LanguageServiceClient.Create();
        //    using (PolitiFactContext db = new PolitiFactContext())
        //    {
        //        var query = db.Tweet.Where(a => a.TwitterName == handle);

        //        List<Tweet> tweets = new List<Tweet>();
        //        foreach (Tweet t in query)
        //        {
        //            tweets.Add(t);
        //        }

        //        return Ok(tweets);
        //    }

        //    return Ok();
        //}
    }
}
