using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem.Models
{
    public class QuestionAndAnswerModel
    {
        public FactModel Question {get;set;}
        public IEnumerable<FactModel> Answers { get; set; }

        public string SelectedAnswer { get; set; }

        /// <summary>
        /// state will be initial / final / middle (need to check this)
        /// </summary>
        public string State { get; set; } 
        /**
         * 
        public string State { get; set; } // state will be initial / final / middle (need to check this)
        public List<string> validAnswers { get; set; } // Yes / No
        public string displayQuestion { get; set; } // will be string from the Clipse
         */
    }
}
 