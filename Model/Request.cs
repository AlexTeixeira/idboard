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
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;

namespace Model
{
    public abstract class Request 
    {
        public static String Url { get; set; }

        public static String Path { get; set; }


        public async Task<String> RunAsync(Object _apiToCall)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string test = JsonConvert.SerializeObject(_apiToCall);
                HttpResponseMessage response = await client.PostAsJsonAsync(Path, _apiToCall);

                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    return stringContent;                 

                }

                return null;


            }
        }


    }
}
