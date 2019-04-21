using System;
using System.Collections.Generic;

namespace BeachHacks.Models
{
    public partial class Categories
    {
        public int CategoryId { get; set; }
        public int TweetId { get; set; }
        public decimal Confidence { get; set; }

        public virtual Tweet Tweet { get; set; }
    }
}
