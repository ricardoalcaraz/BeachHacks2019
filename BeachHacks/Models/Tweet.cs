using System;
using System.Collections.Generic;

namespace BeachHacks.Models
{
    public partial class Tweet
    {
        public int TweetId { get; set; }
        public string Text { get; set; }
        public long TwitterUserId { get; set; }
        public string TwitterName { get; set; }
        public int PoliticalCandidate { get; set; }

        public virtual Presidentialcandidate PoliticalCandidateNavigation { get; set; }
    }
}
