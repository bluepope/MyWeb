using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace MyWeb.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpClientTest();
            RestSharpTest();
        }

        static void HttpClientTest()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;

            request.RequestUri = new Uri("https://localhost:44371/weatherforecast");
            //request.Content = new FormUrlEncodedContent();

            var response = httpClient.SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine(responseText);

                var list = JsonConvert.DeserializeObject<List<WeatherForecastModel>>(responseText);
                
                foreach(var item in list)
                {
                    Console.WriteLine(item.Date);
                }
                
            }
            else
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine(response.StatusCode);
                Console.WriteLine(responseText);
            }
        }

        static void RestSharpTest()
        {
            var restClient = new RestSharp.RestClient();
            var request = new RestRequest();

            request.Resource = "https://localhost:44371/weatherforecast";
            request.Method = Method.GET;
            //request.RequestFormat = DataFormat.None;

            request.AddParameter("num", 4, ParameterType.GetOrPost);

            var response = restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseText = response.Content;


                var list = JsonConvert.DeserializeObject<List<WeatherForecastModel>>(responseText);

                Console.WriteLine(list.Count);

            }
            else
            {
                string responseText = response.Content;

                Console.WriteLine(response.StatusCode);
                Console.WriteLine(responseText);
            }
        }
    }
}
