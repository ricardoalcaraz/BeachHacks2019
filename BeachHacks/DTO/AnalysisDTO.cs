using System.Collections.Generic;
using System;
namespace BeachHacks.DTO
{
    public class AnalysisDTO
    {
        //public ICollection<EntityDTO> Entities { get; set; }
        //public ICollection<CategoryDTO> Categories { get; set; }
        public DateTime? DateOnly { get; set; }
        public double Sentiment_Score { get; set; }
        public double Sentiment_Mag { get; set; }
    }
}
