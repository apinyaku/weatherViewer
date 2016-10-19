using iAsset.WeatherViewer.MVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace iAsset.WeatherViewer.MVC.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void IndexView()
        {
            WeatherController controller = new WeatherController();
            ViewResult result=controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetCountryCities()
        {
            //action
            WeatherController controller = new WeatherController();
            JsonResult result = await controller.GetCountryCities("Australia") as JsonResult;

            //convert data result
            var data = (IEnumerable<iAsset.WeatherViewer.DataModel.City>)result.Data;

            //check if the first data in Model list is Adelaide Airport
            Assert.AreEqual("Adelaide Airport", data.First().CityName);
        }

        [TestMethod]
        public void GetCityWeatherData()
        {
            //action
            WeatherController controller = new WeatherController();
            JsonResult result = controller.GetCityWeather("Sydney") as JsonResult;

            //convert data result
            dynamic jsonObject = JObject.Parse(result.Data.ToString());
            
            //check if the data result name is Sydney
            Assert.AreEqual("Sydney", jsonObject.name.ToString());
        }

    }
}
