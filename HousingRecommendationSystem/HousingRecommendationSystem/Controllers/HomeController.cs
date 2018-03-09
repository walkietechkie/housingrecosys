using System.Web.Mvc;
using HousingRecommendationSystem.Models;

namespace HousingRecommendationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClipsAdapter _clipsAdapter;
        private readonly IDatabaseUtility _databaseUtility;

        public HomeController(IClipsAdapter clipsAdapter, IDatabaseUtility databaseUtility)
        {
            _clipsAdapter = clipsAdapter;
            _databaseUtility = databaseUtility;
        }
        public ActionResult Index()
        {
            var qAndA = GetState();
            string getAnswer = "";
            if (!string.IsNullOrEmpty(qAndA.Question.Id)) {
                getAnswer = qAndA.Question.Id;
                }

            if (qAndA.State == "final" || getAnswer.StartsWith("A"))
            {
                //get all properties for the clips final output
                //var properties = GetProperties(qAndA.Question.Id.Replace("A",""));
                var id = qAndA.Question.Id.Replace("A", "");
                //can reset the Clips
                _clipsAdapter.Reset();
                //todo redirect to maps controller
                return RedirectToAction("Search/"+id, "Maps", id);
            }
            ViewBag.Message = GetState();
            return View();
        }

        [HttpPost]
        public ActionResult SubmitAnswer(QuestionAndAnswerModel qAModel)
        {
            _clipsAdapter.EvaluateQuestionAndAnswer(qAModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Reset()
        {
            _clipsAdapter.Reset();
            return RedirectToAction("Index");
        }

        private QuestionAndAnswerModel GetState()
        {
            return _clipsAdapter.GetQuestionAndAnswer();
        }
    }
}