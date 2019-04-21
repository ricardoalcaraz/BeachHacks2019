using System;
using System.Collections.Generic;

namespace BeachHacks.Models
{
    public partial class Entities
    {
        public int EntityId { get; set; }
        public int TweetId { get; set; }
        public decimal Salience { get; set; }

        public virtual Tweet Tweet { get; set; }
    }
}
