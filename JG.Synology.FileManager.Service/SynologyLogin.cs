using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public interface ISynologyLogin
    {
        SynologyData URIDetails { get; set; }

        string SID { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        void InitialiseLoginInfo(SynologyAPIInfo<SynologyDataSources> apiInfo);
    }

    public class SynologyLogin : ISynologyLogin
    {
        public SynologyData URIDetails { get; set; }

        [JsonProperty("sid")]
        public string SID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public void InitialiseLoginInfo(SynologyAPIInfo<SynologyDataSources> apiInfo)
        {
            this.URIDetails = apiInfo.Data.Data;

            this.Username = "admin";
            this.Password = "gabijustin";
        }
    }
}
