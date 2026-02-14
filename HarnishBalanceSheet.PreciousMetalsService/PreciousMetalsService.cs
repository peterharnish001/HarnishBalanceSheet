using HarnishBalanceSheet.Models;
using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace HarnishBalanceSheet.PreciousMetalsService
{
    public class PreciousMetalsService : IPreciousMetalsService
    {
        private readonly string _apiUrl;

        public PreciousMetalsService(IConfiguration configuration) 
        {
            _apiUrl = configuration["MySettings:ApiUrl"];
        }


        public async Task<IEnumerable<PreciousMetalPrice>> GetPreciousMetalsPricesAsync()
        {
            var result = new List<PreciousMetalPrice>();

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var postData = $"{{\"query\":\"fragment MetalFragment on Metal{{currency name results{{...MetalQuoteFragment}}}}fragment MetalQuoteFragment on Quote{{bid}}query AllMetalsQuote($currency:String!,$timestamp:Int){{gold:GetMetalQuoteV3(symbol:\\\"AU\\\" timestamp:$timestamp currency:$currency){{...MetalFragment}}silver:GetMetalQuoteV3(symbol:\\\"AG\\\" timestamp:$timestamp currency:$currency){{...MetalFragment}}platinum:GetMetalQuoteV3(symbol:\\\"PT\\\" timestamp:$timestamp currency:$currency){{...MetalFragment}}palladium:GetMetalQuoteV3(symbol:\\\"PD\\\" timestamp:$timestamp currency:$currency){{...MetalFragment}}rhodium:GetMetalQuoteV3(symbol:\\\"RH\\\" timestamp:$timestamp currency:$currency){{...MetalFragment}}}}\",\"variables\":{{\"timestamp\":{timestamp},\"currency\":\"USD\"}},\"operationName\":\"AllMetalsQuote\"}}";

            using var httpClient = new HttpClient();

            //string json = JsonSerializer.Serialize(postData);
            
            using var content = new StringContent(postData, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await httpClient.PostAsync(_apiUrl, content);
            
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(jsonString);

            JsonElement root = doc.RootElement;
            root.TryGetProperty("data", out var data);

            foreach (JsonProperty metal in data.EnumerateObject())
            {
                metal.Value.TryGetProperty("name", out var name);
                metal.Value.TryGetProperty("results", out var results);
                results.EnumerateArray().First().TryGetProperty("bid", out var bid);
                result.Add(new PreciousMetalPrice()
                {
                    Metal = name.GetString(),
                    Price = bid.GetDecimal()
                });
            }

            return result;
        }
    }
}
