using Model.classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Request
    {
        public String Url { get; set; }
        public Object ApiToCall { get; set; }

        public String Path { get; set; }

        public Request(String _url, Object _apiToCall,String _path)
        {

            Url = _url;
            ApiToCall = _apiToCall;
            Path = _path;

            RunAsync().Wait();
        }

        public async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(Path, ApiToCall).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    dynamic d = JsonConvert.DeserializeObject(stringContent);

                }

            }
        }
    }
}
