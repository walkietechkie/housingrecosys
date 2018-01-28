using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CLIPSNET;
using HousingRecommendationSystem.Models;

namespace HousingRecommendationSystem.Controllers
{
    public class HomeController : Controller
    {
        private CLIPSNET.Environment clips = new CLIPSNET.Environment();

        public ActionResult Index()
        {
            InitializeClips();
            ViewBag.Message = GetState();
            return View();
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

        private void InitializeClips()
        {
            clips.LoadFromResource(Properties.Resources.recommendation_engine);
            clips.LoadFromResource(Properties.Resources.recommendation_engine_def);
            clips.Reset();
            clips.Run();
        }

        private QuestionAndAnswer GetState()
        {
            String evalStr = "(find-fact ((?f UI-state)) TRUE)";
            FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

            return new QuestionAndAnswer
            {
                Question = fv["display"].ToString()
            };
        }
    }
}