using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.DTOs;
using Web.Models.Interface;

namespace Web.Models
{
    public class MachineApiModel : IMachineApiModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public MachineApiModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<List<Product>> GetProducts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://localhost:5001/Machine/Products");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await System.Text.Json.JsonSerializer.DeserializeAsync
                    <List<Product>>(responseStream);
            }
            return  null;
        }
        
        public async Task<(string,string)> Buy(Inserted inserted)
        {

            var json = JsonConvert.SerializeObject(inserted);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var request = new HttpRequestMessage(){ 
                Content = stringContent,
                Method =  HttpMethod.Post,
                RequestUri = new Uri("https://localhost:5001/Machine/Buy")                
            };
           
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var result = await client.SendAsync(request);

            if (result.IsSuccessStatusCode)
            {
                var response = result.Content.ReadAsStringAsync().Result;
                Dictionary<int,int> dictionary = JsonConvert.DeserializeObject<Dictionary<int,int>>(response);      
                string resultString = "";
                foreach(var coin in dictionary)
                {
                    resultString+= $",[{((float)coin.Key/100)}€ x {coin.Value}]";
                }
                return ("Thank you",resultString);
            }
            else
            {
                var response = result.Content.ReadAsStringAsync().Result;
                string dictionary = JsonConvert.DeserializeObject<string>(response);                      
                return (dictionary,"");
            }
        }           
        
    }
}
