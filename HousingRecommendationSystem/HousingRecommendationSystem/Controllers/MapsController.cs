using HousingRecommendationSystem.Models;
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
        private readonly IDatabaseUtility _databaseUtility;
        string strID = string.Empty;
        // GET: Maps
        public MapsController(IDatabaseUtility databaseUtility)
        {
            _databaseUtility = databaseUtility;
        }
        public ActionResult Index(string ID)
        {
            strID = ID;
            return View();
        }
        [HttpGet]
        public JsonResult GetData()
        {
            IEnumerable<PropertyModel> _pr =  GetProperties(strID);
            List<JsonData> lsJSonData = new List<JsonData>();
            int iRow = 0;
            foreach (PropertyModel pr in _pr)
            {
                lsJSonData.Add(new JsonData { Id = iRow, PlaceName = pr.PropertyName, GeoLat = pr.Latitude.ToString(), GeoLong = pr.Longitude.ToString()});
                iRow++;
            }
            
            //lsJSonData.Add(new JsonData { Id = 1, PlaceName = "Chinese Garden", GeoLong = "1.34065", GeoLat = "103.737472" });
            //lsJSonData.Add(new JsonData { Id = 1, PlaceName = "CC Club", GeoLong = "1.3423597", GeoLat = "103.7325541" });
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(lsJSonData);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        private System.Collections.Generic.IEnumerable<PropertyModel> GetProperties(string bucketId)
        {
            return _databaseUtility.GetPropertyModelByBucketId(bucketId);
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