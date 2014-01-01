using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public class SynologyFileOwner
    {
        [JsonProperty("gid")]
        public int FileGID { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("uid")]
        public int FileUID { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }

    public class SynologyFileTime
    {
        private int lastAccessedSeconds;
        private int lastModifiedSeconds;
        private int lastChangedSeconds;
        private int createdSeconds;

        /// <summary>
        /// Last Accessed time in Linux timestamp seconds
        /// </summary>
        [JsonProperty("atime")]
        public int LastAccessedSeconds
        {
            get
            {
                return lastAccessedSeconds;
            }
            set
            {
                this.lastAccessedSeconds = value;
                this.LastAccessed = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(this.lastAccessedSeconds);
            }
        }

        /// <summary>
        /// Last Modified time in Linux timestamp seconds
        /// </summary>
        [JsonProperty("mtime")]
        public int LastModifiedSeconds
        {
            get
            {
                return lastModifiedSeconds;
            }
            set
            {
                this.lastModifiedSeconds = value;
                this.LastModified = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(this.lastModifiedSeconds);
            }
        }

        /// <summary>
        /// Last Change time in Linux timestamp seconds
        /// </summary>
        [JsonProperty("ctime")]
        public int LastChangedSeconds
        {
            get
            {
                return lastChangedSeconds;
            }
            set
            {
                this.lastChangedSeconds = value;
                this.LastChanged = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(this.lastChangedSeconds);
            }
        }

        /// <summary>
        /// Created time in Linux timestamp seconds
        /// </summary>
        [JsonProperty("crtime")]
        public int CreatedSeconds
        {
            get
            {
                return createdSeconds;
            }
            set
            {
                this.createdSeconds = value;
                this.Created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(this.createdSeconds);
            }
        }

        public DateTime LastAccessed { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime LastChanged { get; set; }

        public DateTime Created { get; set; }
    }

    public class SynologyFileACL
    {
        [JsonProperty("append")]
        public bool AppendEnabled { get; set; }

        [JsonProperty("del")]
        public bool DeleteEnabled { get; set; }

        [JsonProperty("exec")]
        public bool ExecuteEnabled { get; set; }

        [JsonProperty("read")]
        public bool ReadEnabled { get; set; }

        [JsonProperty("write")]
        public bool WriteEnabled { get; set; }
    }

    public class SynologyFilePermission
    {
        [JsonProperty("acl")]
        public SynologyFileACL FileACL { get; set; }

        [JsonProperty("is_acl_mode")]
        public bool IsACLModeEnabled { get; set; }

        // TODO: Convert this number to an enum
        [JsonProperty("posix")]
        public int PosixFilePermission { get; set; }
    }

    public class SynologyFileAdditional
    {
        [JsonProperty("owner")]
        public SynologyFileOwner FileOwner { get; set; }

        [JsonProperty("perm")]
        public SynologyFilePermission FilePermission { get; set; }

        [JsonProperty("real_path")]
        public string FilePath { get; set; }

        [JsonProperty("size")]
        public string FileSize { get; set; }

        [JsonProperty("time")]
        public SynologyFileTime FileTime { get; set; }

        [JsonProperty("type")]
        public string FileType { get; set; }
    }

    public class SynologyFile
    {
        [JsonProperty("name")]
        public string FileName { get; set; }

        [JsonProperty("path")]
        public string FileFullPath { get; set; }

        [JsonProperty("additional")]
        public SynologyFileAdditional FileAdditional { get; set; }

        [JsonProperty("isdir")]
        public bool IsDirectory { get; set; }
    }

    public class SynologyFileList
    {
        [JsonProperty("files")]
        public List<SynologyFile> SynologyFiles { get; set; }

        [JsonProperty("total")]
        public int TotalMatches { get; set; }

        [JsonProperty("finished")]
        public bool FinishedSearch { get; set; }
    }
}
