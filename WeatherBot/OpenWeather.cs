using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WeatherBot
{
    public class OpenWeather
    {
        private const string APIKEY = "[your api key here]";

        public static async Task<CurrentConditions> GetWeatherAsync(string ZipCode)
        {
            Uri ServiceURL = new Uri(string.Format("http://api.openweathermap.org/data/2.5/weather?zip={0}&APPID={1}&units=imperial", ZipCode, APIKEY));
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ServiceURL);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            CurrentConditions conditions = JsonConvert.DeserializeObject<CurrentConditions>(result);

            return conditions;
        }
    }
}