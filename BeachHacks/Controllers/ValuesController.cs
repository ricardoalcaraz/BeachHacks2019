﻿using System;
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
        private readonly PolitiFactContext _context;

        public ValuesController(PolitiFactContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var democrat = _context.Politicalparty.First();
            var presidentialCandidate = _context.Presidentialcandidate.First();
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
