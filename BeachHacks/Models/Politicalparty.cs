using System;
using System.Collections.Generic;

namespace BeachHacks.Models
{
    public partial class Politicalparty
    {
        public Politicalparty()
        {
            Presidentialcandidate = new HashSet<Presidentialcandidate>();
        }

        public int PoliticalPartyId { get; set; }
        public string PartyName { get; set; }

        public virtual ICollection<Presidentialcandidate> Presidentialcandidate { get; set; }
    }
}
