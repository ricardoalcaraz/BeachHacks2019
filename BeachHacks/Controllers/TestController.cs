using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BeachHacks.DAL;
using BeachHacks.DTO;
using BeachHacks.Models;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Language.V1;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeachHacks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //TestDTO response;
            //using (PolitiFactContext db = new PolitiFactContext())
            //{
            //    var democrat = db.Politicalparty.First();
            //    var presidentialCandidate = db.Presidentialcandidate.First();

            //    response = new TestDTO { Party = democrat.PartyName, Candidate = presidentialCandidate.Name };
            //}

            //return Ok(response);

            TestDTO data;
            var client = LanguageServiceClient.Create();
            using (PolitiFactContext db = new PolitiFactContext())
            {
                string post = db.Tweet.First().Text;
                var response = client.AnalyzeSentiment(new Document()
                {
                    Content = post,
                    Type = Document.Types.Type.PlainText
                });

                var sentiment = response.DocumentSentiment;

                data = new TestDTO { Content = post, Score = sentiment.Score , Magnitude = sentiment.Magnitude };
            }

            return Ok(data);
        }

        [HttpGet("{handle}")]
        public ActionResult<IEnumerable<string>> Get(string handle)
        {
            TestDTO data;
            var client = LanguageServiceClient.Create();
            using (PolitiFactContext db = new PolitiFactContext())
            {
                var query = db.Tweet.Where(a => a.TwitterName == handle);

                List<Tweet> tweets = new List<Tweet>();
                foreach (Tweet t in query) {
                    tweets.Add(t);
                }

                return Ok(tweets);
            }

            return Ok();
        }
    }
}
