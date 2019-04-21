﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeachHacks.DAL;
using BeachHacks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeachHacks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        Dictionary<int, string> CandidateImages = new Dictionary<int, string>();

        public HomeController()
        {
            CandidateImages.Add(1, "..\\ClientApp\\src\\assets\\ElizabethWarrenProfile.jpg");
        }

        [HttpGet]
        [Route("GetPresidentialCandidates")]
        public JsonResult GetPresidentialCandidates()
        {
            using (PolitiFactContext db = new PolitiFactContext())
            {
                var candidates = db.Presidentialcandidate
                    .Include(p => p.PoliticalParty)
                    .Select(p => new { p.Name, p.Age, p.PoliticalParty.PartyName, p.State})
                    .ToList();

                return Json(candidates);
            }

        }
    }
}