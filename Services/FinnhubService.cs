using System;
using System.Text.Json;
using FinnhubApp.ServicesContract;
using Microsoft.Extensions.Configuration;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

		public FinnhubService(IHttpClientFactory clientFactory, IConfiguration config)
		{
            _httpClientFactory = clientFactory;
            _config = config;
		}

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_config["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                Stream stream = responseMessage.Content.ReadAsStream();
                StreamReader sr = new StreamReader(stream);
                string responseStr = sr.ReadToEnd();

                // Convert response to dictionary
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseStr);

                // handle error
                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from finnhub server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(responseDictionary["error"].ToString());

                return responseDictionary;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_config["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                Stream stream = responseMessage.Content.ReadAsStream();
                StreamReader sr = new StreamReader(stream);
                string responseStr = sr.ReadToEnd();

                // Convert response to dictionary
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseStr);

                // handle error
                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from finnhub server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(responseDictionary["error"].ToString());

                return responseDictionary;
            }
        }
    }
}

