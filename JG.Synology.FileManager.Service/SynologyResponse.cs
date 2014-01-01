using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public interface ISynologyRequest<T>
    {
        /// <summary>
        /// Send request to Synology. A JSon object is returned and is mapped to a .Net object.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        SynologyAPIInfo<T> SendRequest(string queryString);
    }

    public class SynologyRequest<T> : ISynologyRequest<T>
    {
        private HttpClient client;

        public SynologyRequest(HttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Send request to Synology. A JSon object is returned and is mapped to a .Net object.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public SynologyAPIInfo<T> SendRequest(string queryString)
        {
            SynologyAPIInfo<T> responseMappedJson = null;

            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonSynologyAPIResponse = GetResponseJsonText(response);

                responseMappedJson = (SynologyAPIInfo<T>)JsonConvert.DeserializeObject<SynologyAPIInfo<T>>(jsonSynologyAPIResponse);
            }

            return responseMappedJson;
        }

        public static string GetResponseJsonText(HttpResponseMessage response)
        {
            var apiInfoStream = response.Content.ReadAsStreamAsync().Result;

            TextReader standardOutput = new StreamReader(apiInfoStream);
            string jsonSynologyAPIResponse = standardOutput.ReadToEnd();

            System.Diagnostics.Debug.WriteLine(jsonSynologyAPIResponse);

            return jsonSynologyAPIResponse;
        }
    }
}
