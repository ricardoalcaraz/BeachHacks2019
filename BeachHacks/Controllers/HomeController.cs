using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BeachHacks.DAL;
using BeachHacks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace BeachHacks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly PolitiFactContext _context;

        public HomeController(PolitiFactContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetPresidentialCandidates")]
        public JsonResult GetPresidentialCandidates()
        {
            var candidates = _context.Presidentialcandidate
                .Include(p => p.PoliticalParty)
                .Select(p =>
                new {
                    p.Name,
                    p.Age,
                    p.PoliticalParty.PartyName,
                    p.State,
                    p.UserId,
                    location = "../assets/" + p.Name.Replace(" ", "") + ".jpg",
                    twitterHandle = _context.Tweet.FirstOrDefault(a => a.PoliticalCandidate == p.UserId).TwitterName
                })
                .ToList();

            return Json(candidates);
        }

        [HttpGet]
        [Route("GetSentimentScore/{id}")]
        public JsonResult GetSentimentScore(int id)
        {
            JsonResult returnVal = null;
            try
            {

                var score = from entity in _context.Entities
                    join tweet in _context.Tweet on entity.TweetId equals tweet.TweetId
                    where tweet.PoliticalCandidate == id
                    select entity;
                var avgSentimentScore = score.Average(a => a.SentimentScore);
                var avgMagnitude = score.Average(a => a.SentimentMag);

                var groupedTweetsByYear = score.GroupBy(a => a.Tweet.Time.Value.Year);
                var returnV = new { avgSentimentScore, avgMagnitude, Count = score.Count()};
                returnVal = Json(returnV);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return returnVal;

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("CandidateInfo/{id}")]
        [HttpGet]
        public ActionResult CandidateInfo(string name)
        {
            ActionResult returnVal;

            var candidate = _context.Presidentialcandidate.FirstOrDefault(p => p.Name == name);
            returnVal = View(candidate);
            ViewBag.Candidate = candidate;

            return returnVal;
        }
    }
}