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

            if(qAndA.State == "final")
            {
                //get all properties for the clips final output
                //var properties = GetProperties(qAndA.Question.Id.Replace("A",""));
                var id = qAndA.Question.Id.Replace("A", "");
                //todo redirect to maps controller
                return RedirectToAction("Index/"+id, "Maps", id);
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
            ViewBag.Message = "Housing Recommendation System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We will be reachable at";

            return View();
        }

        private QuestionAndAnswerModel GetState()
        {
            return _clipsAdapter.GetQuestionAndAnswer();
        }
    }
}