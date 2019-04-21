using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeachHacks.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BeachHacks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using(PolitiFactContext db = new PolitiFactContext())
            {
                var democrat = db.Politicalparty.First();
                var presidentialCandidate = db.Presidentialcandidate.First();
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
