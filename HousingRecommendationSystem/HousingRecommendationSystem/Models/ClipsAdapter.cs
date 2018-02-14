using System;

namespace HousingRecommendationSystem.Models
{
    public class ClipsAdapter : IClipsAdapter
    {
        private CLIPSNET.Environment _clips = new CLIPSNET.Environment();

        public void EvaluateQuestionAndAnswer(QuestionAndAnswerModel questionAndAnswer)
        {
            throw new NotImplementedException();
        }

        public QuestionAndAnswerModel GetQuestionAndAnswer()
        {
            throw new NotImplementedException();
        }
    }
}