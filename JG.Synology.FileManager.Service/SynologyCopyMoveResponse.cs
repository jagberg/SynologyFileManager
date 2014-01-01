using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public class SynologyCopyMoveResponse
    {
        [JsonProperty("processed_size")]
        public int ProcessedSize { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("dest_folder_path")]
        public string DestinationPath { get; set; }

        [JsonProperty("progress")]
        public double Progress { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }
    }
}
