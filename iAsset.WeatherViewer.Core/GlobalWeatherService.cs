using iAsset.WeatherViewer.DataModel;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace iAsset.WeatherViewer.Core
{
    public class GlobalWeatherService
    {

        //Fetch list of Cities of a Country using the Global Weather Web Service (SOAP) (www.webservicex.net/globalweather.asmx)
        public async Task<IEnumerable> GetCountryCities(string countryName)
        {
            try
            {
                if (countryName == null) return null;
                svcGlobalWeather.GlobalWeatherSoapClient svc = new svcGlobalWeather.GlobalWeatherSoapClient("GlobalWeatherSoap");
                string response = await svc.GetCitiesByCountryAsync(countryName);
                if (response != null)
                {
                    var xml = XDocument.Parse(response);
                    //select only City from response data
                    var c = xml.Descendants("Table").Select(i => new City { CityName = i.Element("City").Value }).OrderBy(i => i.CityName).AsEnumerable();
                    return c;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
