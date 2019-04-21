using System.Collections.Generic;
using Google.Cloud.Language.V1;

namespace BeachHacks.DTO
{
    public class EntityDTO
    {
        public string Name { get; set; }
        public Entity.Types.Type Type { get; set; }
        public float Salience { get; set; }
        public float Sentiment_Score { get; set; }
        public float Sentiment_Mag { get; set; }
    }
}
