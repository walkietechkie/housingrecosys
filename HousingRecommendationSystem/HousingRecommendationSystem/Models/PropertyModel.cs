using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem.Models
{
    public class PropertyModel
    {
        public string PropertyName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BucketId { get; set; }
    }
}