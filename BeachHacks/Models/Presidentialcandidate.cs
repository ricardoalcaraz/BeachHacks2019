using System;
using System.Collections.Generic;

namespace BeachHacks.Models
{
    public partial class Presidentialcandidate
    {
        public Presidentialcandidate()
        {
            Tweet = new HashSet<Tweet>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public int PoliticalPartyId { get; set; }

        public virtual Politicalparty PoliticalParty { get; set; }
        public virtual ICollection<Tweet> Tweet { get; set; }
    }
}
