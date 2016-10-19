using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iAsset.WeatherViewer.Core;
using System.Threading.Tasks;

namespace iAsset.WeatherViewer.MVC.Controllers
{
    public class WeatherController : Controller
    {
        //
        // GET: /Weather/

        public ActionResult Index()
        {
            return View();
        }

        //action to get Cities of a Country
        //I used the method from iAsset.WeatherViewer.Core.GlobalWeatherService class
        public async Task<ActionResult> GetCountryCities(string cName)
        {
            try
            {
                if (cName == null) throw (new Exception("Country Name is not given"));

                GlobalWeatherService svc = new GlobalWeatherService();
                var response = await svc.GetCountryCities(cName);

                if (response != null)
                {
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                throw (new Exception("No Response from GlobalWeatherService"));
            }
            catch (Exception ex)
            {

                return ThrowJsonError(ex);
            }


        }

        //action to get Weather details of a City
        //I used the method from iAsset.WeatherViewer.Core.OpenWeatherMapService class
        public ActionResult GetCityWeather(string cName)
        {

            try
            {
                if (cName == null) throw (new Exception("City Name is not given"));

                OpenWeatherMapService svc = new OpenWeatherMapService();
                var response = svc.GetCityWeather(cName);

                if (response != null)
                {
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                throw (new Exception("No Response from OpenWeatherMapService"));
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }

        }

        //Private method to be able to throw error in View + $.ajax + fail event
        private JsonResult ThrowJsonError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;

            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}
