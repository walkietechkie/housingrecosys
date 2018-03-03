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
                //var properties = GetProperties("111110010000");

                //todo redirect to maps controller
                return RedirectToAction("Index");
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

        private System.Collections.Generic.IEnumerable<PropertyModel> GetProperties(string bucketId)
        {
            return _databaseUtility.GetPropertyModelByBucketId(bucketId);
        }
    }
}