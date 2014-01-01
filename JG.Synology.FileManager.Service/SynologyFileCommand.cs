using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG.Synology.FileManager.Service
{
    public struct SynologyFileCommand
    {
        public const string LISTFILES = "SYNO.FileStation.List";

        public const string FILESEARCH = "SYNO.FileStation.Search";

        public const string FILECOPYMOVE = "SYNO.FileStation.CopyMove";
    }
}
