using HousingRecommendationSystem.Resources;
using Mommosoft.ExpertSystem;
using System;
using System.Collections.Generic;

namespace HousingRecommendationSystem.Models
{
    public class ClipsAdapter : IClipsAdapter
    {
        private Mommosoft.ExpertSystem.Environment _clipsEnvironment = new Mommosoft.ExpertSystem.Environment();
        private QuestionAndAnswerModel _qAndA;

        public ClipsAdapter(IFileManager fileManager)
        {
            var filePath = fileManager.GetClipsFilePath();

            _clipsEnvironment.AddRouter(new DebugRouter());
            _clipsEnvironment.Load(filePath);
            _clipsEnvironment.Reset();

            Initialize();
        }

        private void Initialize()
        {
            _qAndA = GetQuestionAndAnswerModelInternal("Restart", string.Empty, true);
        }

        public void EvaluateQuestionAndAnswer(QuestionAndAnswerModel questionAndAnswer)
        {
            var buttonPressed = questionAndAnswer.State == "final" ? "Restart" : "Next";
            _qAndA = GetQuestionAndAnswerModelInternal(buttonPressed, questionAndAnswer.SelectedAnswer == null ? string.Empty : questionAndAnswer.SelectedAnswer );
        }

        public QuestionAndAnswerModel GetQuestionAndAnswer()
        {
            return _qAndA;
        }

        /* call this method from the fron end. D:\school\nus\unit1\project\git\HousingRecommendationSystem\HousingRecommendationSystem\Models\ClipsAdapter.cs
         * input = strButtonName = Next/Restart/Prev
         * initialFlag = call this for the first time
         * ChoiceOption = Yes / No
         */
        private QuestionAndAnswerModel GetQuestionAndAnswerModelInternal(string strButtonName, string ChoiceOption, bool initialFlag = false)
        {
            //prep clips state here
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)_clipsEnvironment.Eval(Properties.Resources.FindStateList))[0])
            {
                string currentID = f.GetFactSlot("current").ToString();
                if (strButtonName.Equals("Next"))
                {
                    if (initialFlag)
                    {
                        _clipsEnvironment.AssertString("(next " + currentID + ")");
                    }
                    else
                    {
                        _clipsEnvironment.AssertString("(next " + currentID + " " + ChoiceOption + ")");
                    }
                }
                else if (strButtonName.Equals("Restart"))
                {
                    _clipsEnvironment.Reset();
                }
                else if (strButtonName.Equals("Prev"))
                {
                    _clipsEnvironment.AssertString("(prev " + currentID + ")");
                }
                
                //end prep
                return getNextUIState();
            }
        }

        public QuestionAndAnswerModel getNextUIState()
        {
            //run the clips first
            _clipsEnvironment.Run();

            var returnValue = new QuestionAndAnswerModel();
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue allFacts = (FactAddressValue)((MultifieldValue) _clipsEnvironment.Eval(evalStr))[0])
            {
                string currentID = allFacts.GetFactSlot("current").ToString();
                // update query to get all facts based on current id
                evalStr = "(find-all-facts ((?f UI-state)) " +
                               "(eq ?f:id " + currentID + "))";
            }

            //re eval using updated query
            using (FactAddressValue evalFact = (FactAddressValue)((MultifieldValue) _clipsEnvironment.Eval(evalStr))[0])
            {
                //get the state from clipse
                string state = evalFact.GetFactSlot("state").ToString();
                returnValue.State = state;
                using (MultifieldValue validAnswers = (MultifieldValue)evalFact.GetFactSlot("valid-answers"))
                {
                    var answerList = new List<FactModel>();
                    for (int i = 0; i < validAnswers.Count; i++)
                    {
                        var ans = (SymbolValue)validAnswers[i];
                        // assume ans value is same for UI display and ID
                        answerList.Add(new FactModel(ans, ans));
                    }
                    returnValue.Answers = answerList;
                }
                // assume question value is same for UI display and ID
                var question = (SymbolValue)evalFact.GetFactSlot("display");
                returnValue.Question = new FactModel(GetDisplayText(question), question);
            }
            return returnValue;
        }

        private string GetDisplayText(string id)
        {
            return ExpSysResources.ResourceManager.GetString(id);
        }
    }
}