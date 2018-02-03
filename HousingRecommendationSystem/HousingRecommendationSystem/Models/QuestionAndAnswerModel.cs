using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem.Models
{
    public class QuestionAndAnswerModel
    {
        public string Question { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }

        public string SelectedAnswer { get; set; }
    }
}