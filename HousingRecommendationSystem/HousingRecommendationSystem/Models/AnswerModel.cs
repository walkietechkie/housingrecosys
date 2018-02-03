using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem.Models
{
    public class AnswerModel
    {
        public AnswerModel(string displayText, string answerId)
        {
            DisplayText = displayText;
            AnswerId = answerId;
        }

        public string DisplayText { get; set;  }
        public string AnswerId { get; set; }

        public bool IsSelected { get; set; }
    }
}