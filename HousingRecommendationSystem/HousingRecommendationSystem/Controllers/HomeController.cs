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
        private CLIPSNET.Environment _clips = new CLIPSNET.Environment();
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
            ProcessAnswer(qAModel);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Housing Recommendation System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We will be reachable at";

            return View();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            InitializeClips();
            base.Initialize(requestContext);
        }

        private void InitializeClips()
        {
            _clips.LoadFromResource(Properties.Resources.recommendation_engine);
            _clips.LoadFromResource(Properties.Resources.recommendation_engine_def);
            _clips.Reset();
            _clips.Run();

        }

        private QuestionAndAnswerModel GetState()
        {
            var evalStr = Properties.Resources.FindFact;
            var fv = (FactAddressValue)((MultifieldValue)_clips.Eval(evalStr))[0];
            var state = fv["state"];

            var whatisthis = ((LexemeValue)fv["relation-asserted"]).Value;
            return new QuestionAndAnswerModel
            {
                QuestionId = whatisthis,
                Question = fv["display"].ToString(),
                Answers = GetAnswerChoices(fv)
            };
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

        private void ProcessAnswer(QuestionAndAnswerModel qAModel)
        {
            _clips.Reset();
            _clips.Eval(string.Format(Properties.Resources.AssertAnswer, qAModel.QuestionId, qAModel.SelectedAnswer));
            _clips.Run();
        }
    }
}