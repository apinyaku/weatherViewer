using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace iAsset.WeatherViewer.Core
{
    public class OpenWeatherMapService
    {
        //Fetch the Weather detail of a CIty using the JSON Web Service (api.openweathermap.org)
        public string GetCityWeather(string cName)
        {

            try
            {
                if (cName == null) return null;
                string j = string.Empty;

                using (var client = new WebClient())
                {
                    //need to add parameters and pass my own application id (required for authentication)
                    //need to sign up from openweathermap.org to get a unique app id, but it's free
                    NameValueCollection p = new NameValueCollection();
                    p.Add("q", cName);
                    p.Add("appid", "c513426c4017fd300701689789a3f412");
                    p.Add("units", "metric");
                    client.QueryString = p;

                    j = client.DownloadString("http://api.openweathermap.org/data/2.5/weather");
                }

                return j;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
