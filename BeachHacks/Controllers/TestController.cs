using System.Collections.Generic;
using System.Linq;
using BeachHacks.DAL;
using BeachHacks.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            TestDTO response;
            using (PolitiFactContext db = new PolitiFactContext())
            {
                var democrat = db.Politicalparty.First();
                var presidentialCandidate = db.Presidentialcandidate.First();

                response = new TestDTO { Party = democrat.PartyName, Candidate = presidentialCandidate.Name };
            }

            return Ok(response);
        }
    }
}
