using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CLIPSNET;
using HousingRecommendationSystem.Models;

namespace HousingRecommendationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClipsAdapter _clipsAdapter;

        public HomeController(IClipsAdapter clipsAdapter)
        {
            _clipsAdapter = clipsAdapter;
        }
        public ActionResult Index()
        {
            ViewBag.Message = GetState();
            return View();
        }

        [HttpPost]
        public ActionResult SubmitAnswer(QuestionAndAnswerModel qAModel)
        {
            // todo: retrieve answer and pass back to clips
            // then get state again to process next rule
            _clipsAdapter.EvaluateQuestionAndAnswer(qAModel);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private QuestionAndAnswerModel GetState()
        {
            return _clipsAdapter.GetQuestionAndAnswer();
        }

        private IEnumerable<AnswerModel> GetAnswerChoices(FactAddressValue fact)
        {
            var answers = new List<AnswerModel>();
            var displayTexts = (MultifieldValue) fact["display-answers"];
            var answerIds = (MultifieldValue)fact["valid-answers"];

            int iterator = 0;
            foreach (LexemeValue choice in displayTexts)
            {
                answers.Add(new AnswerModel(choice.Value, ((LexemeValue) answerIds[iterator++]).Value));
            }

            return answers;
        }
    }
}