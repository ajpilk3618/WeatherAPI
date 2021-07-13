using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?";

            //api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=0cf647272a5dcd30aa34af83fd21c272

            Console.Write("Please enter a city or zip code:");
            string Zip = Console.ReadLine();
            string getLocation = $"q={Zip}";

            string AppID = "&APPID=";

            string key = File.ReadAllText("appsettings.json");
            JObject jObject = JObject.Parse(key);
            JToken token = jObject["ApiKey"];
            string apiKey = token.ToString();



            string apiCall = $"{url}{getLocation}{AppID}{apiKey}";


            var httpRequest = new HttpClient();

            Task<string> response = httpRequest.GetStringAsync(apiCall);
            Console.WriteLine(response.Result);
            Console.WriteLine("");

            string newResponse = response.Result;
            JObject jObject2 = JObject.Parse(newResponse);
            JToken token2 = jObject2["main"]["temp"];
            var main = token2.ToString();
            Console.WriteLine(main);

            string cityResponse = response.Result;
            JObject jObject3 = JObject.Parse(cityResponse);
            JToken token3 = jObject3["name"];
            var city = token3.ToString();

            var kelvin = Convert.ToDouble(main);
            var temp = Convert.ToInt32((kelvin - 273.15) * 9 / 5 + 32);

            Console.WriteLine($"Temperature in {city} is currently {temp} degrees fahrenheit.");

            Console.ReadLine();
        }
    }
}
