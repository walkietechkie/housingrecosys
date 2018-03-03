using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HousingRecommendationSystem.Controllers
{
    public class MapsController : Controller
    {
        // GET: Maps
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetData()
        {
            List<JsonData> lsJSonData = new List<JsonData>();
            lsJSonData.Add(new JsonData { Id = 1, PlaceName = "Chinese Garden", GeoLong = "1.34065", GeoLat = "103.737472" });
            lsJSonData.Add(new JsonData { Id = 1, PlaceName = "CC Club", GeoLong = "1.3423597", GeoLat = "103.7325541" });
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(lsJSonData);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
    public class JsonData
    {
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }
    }
}