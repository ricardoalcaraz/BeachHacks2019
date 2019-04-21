using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Language.V1;
using BeachHacks.DTO;

namespace BeachHacks.Services
{
    public class AnalyticsEngine
    {
        private const int API_TOKEN_MIN = 20;

        public AnalyticsEngine()
        {
        }


        public bool isTokenSufficient(string text)
        {
            int a = 0; 
            int count = 0;

            while (a <= text.Length - 1)
            {
                if (text[a] == ' ' || text[a] == '\n' || text[a] == '\t')
                {
                    count++;
                }
                a++;
            }

            if (count > API_TOKEN_MIN)
                return true;
            else
                return false;
        }

        public List<EntityDTO> getAnalysis(IEnumerable<Entity> entities)
        {
            List<EntityDTO> eresponse = new List<EntityDTO>();
            foreach (Entity e in entities)
            {
                EntityDTO entityData = new EntityDTO
                {
                    Name = e.Name,
                    Type = e.Type,
                    Salience = e.Salience,
                    Sentiment_Score = e.Sentiment.Score,
                    Sentiment_Mag = e.Sentiment.Magnitude
                };

                eresponse.Add(entityData);
            }

            //List<CategoryDTO> cresponse = new List<CategoryDTO>();
            //if (!categories.Any())
            //{
            //    foreach (ClassificationCategory c in categories)
            //    {
            //        CategoryDTO categoryData = new CategoryDTO
            //        {
            //            Category = c.Name,
            //            Confidence = c.Confidence
            //        };

            //        cresponse.Add(categoryData);
            //    }
            //}


            return eresponse;
        }
    }
}
