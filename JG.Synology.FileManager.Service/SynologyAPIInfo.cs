using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public class SynologyDataSources
    {
        [JsonProperty("SYNO.API.Auth")]
        public SynologyData Data { get; set; }
    }

    public class SynologyData
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("minVersion")]
        public int MinVersion { get; set; }

        [JsonProperty("maxVersion")]
        public int MaxVersion { get; set; }
    }

    public class SynologyAPIInfo<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
