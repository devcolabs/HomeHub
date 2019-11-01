using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeHubApp.Common
{
    public class HubApiStatus
    {
        public bool Ok { get; set; }
        public int Level { get; set; }
    }
    
    public class HubClientService
    {
        private IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        private string _baseUrl = "https://localhost:5001";

        public HubClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<HubApiStatus> SendStatusRequestAsync(string address)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/status/{address}");
            var response = await _httpClient.SendAsync(request);

            HubApiStatus status = new HubApiStatus() { Ok = false, Level = 0 };

            dynamic json;

            if (response.IsSuccessStatusCode)
            {
                var t = await response.Content.ReadAsStringAsync();
                json = JObject.Parse(t);

                var okText = json.Value<string>("ok");
                var levelText = json.Value<string>("level");

                bool ok;
                int level;
                status.Ok = bool.TryParse(json.Value<string>("ok"), out ok) ? ok : false;
                status.Level = int.TryParse(json.Value<string>("level"), out level) ? level : 0;
            }

            return status;
        }

        public async Task<dynamic> SendCommnadRequestAsync(string address, string command, string data="0")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/command/{address}/{command}/{data}");
            var response = await _httpClient.SendAsync(request);

            dynamic json;

            if (response.IsSuccessStatusCode)
            {
                var t = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject(t);
            }
            else
            {
                json = new { ok = false };
            }

            return json;
        }

        public async Task<dynamic> SendGroupRequestAsync(string group, string command, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/group/{group}/{command}/{data}");
            var response = await _httpClient.SendAsync(request);

            dynamic json;
            
            if(response.IsSuccessStatusCode) 
            {
                var t = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject(t);
            }
            else
            {
                json = new { ok = false };
            }

            return json;
        }

    }
}
