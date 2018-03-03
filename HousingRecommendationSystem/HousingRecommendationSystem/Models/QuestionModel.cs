using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem.Models
{
    public class FactModel
    {
        public FactModel(string display, string id)
        {
            DisplayText = display;
            Id = id;
        }
        public string Id { get; }
        public string DisplayText { get; }
    }
}