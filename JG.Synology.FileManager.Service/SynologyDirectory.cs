using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JG.Synology.CrossCutting;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace JG.Synology.FileManager.Service
{
    public class SynologyDirectory
    {
        private readonly string diskStationURL = @"http://192.168.0.5:5000";

        private HttpClient client;
        private ILogger logger;
        private ISynologyLogin synologyLogin;

        private bool IsLoggedIn { get; set; }

        public SynologyDirectory()
        {
            this.client = new HttpClient();
            this.logger = new Logger();
            this.synologyLogin = new SynologyLogin();

            InitializeConnection();
        }

        public SynologyDirectory(HttpClient client, ILogger logger, ISynologyLogin synologyLogin)
        {
            this.client = client;
            this.logger = logger;
            this.synologyLogin = synologyLogin;

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            client.BaseAddress = new Uri(this.diskStationURL);


            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
        }

        //private static void EnforceJSONResponse()
        //{

        //    HttpConfiguration config = GlobalConfiguration.Configuration;
        //    foreach (var mediaType in config.Formatters.FormUrlEncodedFormatter.SupportedMediaTypes)
        //    {
        //        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(mediaType);
        //    }
        //    config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
        //    config.Formatters.Remove(config.Formatters.XmlFormatter);
        //}

        public void GetAPIList()
        {

            try
            {
                this.logger.Log("Start connection");

                // List all products.
                HttpResponseMessage response = client.GetAsync("/webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=all").Result;  // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    object obj;
                    //var info = response.Content(typeof(object));
                    //var products = response.Content.ReadAsAsync<SynologyData>().Result;
                    var products = response.Content.ReadAsStringAsync().Result;
                    foreach (var p in products)
                    {
                        //Console.WriteLine(p.data);
                        //Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.Price, p.Category);
                    }
                }

            }
            catch (Exception ex)
            {
                this.logger.Log(ex.ToString());
            }
        }

        public void GetFileAPIList()
        {
            try
            {
                this.logger.Log("Start connection");

                // List all products.
                string queryString = "/webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=SYNO.API.Auth,SYNO.FileStation";

                SynologyAPIInfo<SynologyDataSources> apiInfo = new SynologyRequest<SynologyDataSources>(client).SendRequest(queryString);

                if (apiInfo.Success)
                {
                    this.synologyLogin.InitialiseLoginInfo(apiInfo);
                }
            }
            catch (Exception ex)
            {
                this.logger.Log(ex.ToString());
            }

        }

        public void Login()
        {
            //string loginURI = string.Format("/webapi/auth.cgi?api=SYNO.API.Auth&version=3&method=login&account=admin&passwd=12345&session=FileStation&format=cookie");
            string queryString = string.Format("/webapi/{0}?api=SYNO.API.Auth&version={1}&method=login&account={2}&passwd={3}&session=FileStation&format=cookie", this.synologyLogin.URIDetails.Path, this.synologyLogin.URIDetails.MaxVersion, this.synologyLogin.Username, this.synologyLogin.Password);

            SynologyAPIInfo<SynologyLogin> loginSID = new SynologyRequest<SynologyLogin>(client).SendRequest(queryString);

            if (loginSID.Success)
            {
                this.synologyLogin.SID = loginSID.Data.SID;

                this.IsLoggedIn = true;
            }
        }

        public List<string> GetFolderList()
        {
            string queryString = string.Format("/webapi/FileStation/file_share.cgi?api={0}&version={1}&method=list_share&additional=real_path%2Cowner%2Ctime", SynologyFileCommand.LISTFILES, 1);

            SynologyAPIInfo<SynologyLogin> loginSID = new SynologyRequest<SynologyLogin>(client).SendRequest(queryString);

            if (loginSID.Success)
            {
                this.synologyLogin.SID = loginSID.Data.SID;
            }

            return null;

        }

        public SynologyAPIInfo<SynologyFileList> GetFiles(string searchName)
        {
            SynologyAPIInfo<SynologyTaskResponse> searchResponse = StartSearch(searchName);

            return null;
        }

        private SynologyAPIInfo<SynologyTaskResponse> StartSearch(string searchName)
        {
            string queryString = string.Format("/webapi/FileStation/file_find.cgi?api={0}&version={1}&method=start&folder_path=%2Fvideo&recursive=true&pattern=1&_sid={2}",
                SynologyFileCommand.FILESEARCH, 1, this.synologyLogin.SID);

            SynologyAPIInfo<SynologyTaskResponse> searchResponse = new SynologyRequest<SynologyTaskResponse>(client).SendRequest(queryString);

            SynologyAPIInfo<SynologyFileList> searchFiles;
            while (true)
            {
                searchFiles = GetSearchResults(searchResponse.Data.TaskID);
                CancelSearch(searchResponse.Data.TaskID);
                ClearSearch(searchResponse.Data.TaskID);
                if (searchFiles.Data.FinishedSearch)
                {

                    break;
                }
            }

            return searchResponse;
        }

        private SynologyAPIInfo<SynologyFileList> GetSearchResults(string searchID)
        {
            string queryString = string.Format("/webapi/FileStation/file_find.cgi?api={0}&version=1&method=list&taskid={1}&_sid={2}",
                SynologyFileCommand.FILESEARCH, searchID, this.synologyLogin.SID);

            SynologyAPIInfo<SynologyFileList> searchResponse = new SynologyRequest<SynologyFileList>(client).SendRequest(queryString);

            return searchResponse;
        }

        /// <summary>
        /// Cancel all searches.
        /// </summary>
        private bool CancelSearch(string searchID)
        {
            string queryString = string.Format("/webapi/FileStation/file_find.cgi?api={0}&version=1&method=clean&taskid={1}&_sid={2}",
                SynologyFileCommand.FILESEARCH, searchID, this.synologyLogin.SID);

            SynologyAPIInfo<SynologyFileList> searchResponse = new SynologyRequest<SynologyFileList>(client).SendRequest(queryString);

            return searchResponse.Success;
        }

        private bool ClearSearch(string searchID)
        {
            string queryString = string.Format("/webapi/FileStation/file_find.cgi?api={0}&version=1&method=stop&taskid={1}&_sid={2}",
                SynologyFileCommand.FILESEARCH, searchID, this.synologyLogin.SID);

            SynologyAPIInfo<SynologyFileList> searchResponse = new SynologyRequest<SynologyFileList>(client).SendRequest(queryString);

            return searchResponse.Success;
        }

        public SynologyAPIInfo<SynologyFileList> GetVideoFileList()
        {
            string queryString = string.Format("/webapi/FileStation/file_share.cgi?api={0}&version={1}&method=list&additional=real_path%2Csize%2Cowner%2Ctime%2Cperm%2Ctype&folder_path=%2Fvideo", SynologyFileCommand.LISTFILES, 1);

            SynologyAPIInfo<SynologyFileList> files = new SynologyRequest<SynologyFileList>(client).SendRequest(queryString); ;

            return files;
        }

        public void MoveFiles(List<SynologyFile> fileList, string destDirName)
        {
            // Only  move file which have a desitination set
            if (destDirName.Length > 0)
            {
                StringBuilder files = new StringBuilder();

                foreach (var file in fileList)
                {
                    files.Append(file.FileFullPath);
                    files.Append(",");
                }

                // Remove last ,
                if (files.Length > 0)
                {
                    files.Remove(files.Length - 1, 1);
                }

                MoveFile(files.ToString(), destDirName);
            }
        }

        private void MoveFile(string sourceFiles, string destDirName)
        {
            string queryString = string.Format("/webapi/FileStation/file_MVCP.cgi?api={0}&version={1}&method=start&path={2}&dest_folder_path={3}&overwrite=true&remove_src=true",
                SynologyFileCommand.FILECOPYMOVE, 1, sourceFiles, destDirName);

            SynologyAPIInfo<SynologyTaskResponse> copyMoveTask = new SynologyRequest<SynologyTaskResponse>(client).SendRequest(queryString);

            SynologyCopyMoveResponse copyMoveStatus;
            while (true)
            {
                copyMoveStatus = CheckFileMoveStatus(copyMoveTask.Data.TaskID);

                if (copyMoveStatus.Finished)
                {
                    break;
                }
            }
        }

        private SynologyCopyMoveResponse CheckFileMoveStatus(string taskID)
        {
            string queryString = string.Format("/webapi/FileStation/file_MVCP.cgi?api={0}&version={1}&method=status&taskid={2}",
                SynologyFileCommand.FILECOPYMOVE, 1, taskID);

            SynologyAPIInfo<SynologyCopyMoveResponse> copyStatus = new SynologyRequest<SynologyCopyMoveResponse>(client).SendRequest(queryString);

            return copyStatus.Data;
        }
    }
}
