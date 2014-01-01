using System;
using JG.Synology.FileManager.Service;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace JG.Synology.FileManager.UnitTests
{
    [TestFixture]
    public class SynologyFileTests
    {
        [Test]
        public void when_a_request_is_made_to_api_return_info()
        {
            SynologyDirectory sort = new SynologyDirectory();
            sort.GetAPIList();
        }

        [Test]
        public void when_a_request_is_made_to_file_api_return_info()
        {
            SynologyDirectory sort = new SynologyDirectory();
            sort.GetFileAPIList();
        }

        [Test]
        public void when_user_logs_in_the_sid_is_saved()
        {
            SynologyDirectory sort = new SynologyDirectory();

            sort.GetFileAPIList();
            sort.Login();

            // Check sid is applied
        }

        [Test]
        public void when_files_are_listed_they_return_values()
        {
            SynologyDirectory sort = new SynologyDirectory();

            sort.GetFileAPIList();
            sort.Login();
            var files = sort.GetVideoFileList().Data.SynologyFiles.FindAll(x => x.FileName.Contains("Middle"));

            // Check sid is applied
        }

        [Test]
        public void when_move_is_called_no_error_is_thrown()
        {
            SynologyDirectory sort = new SynologyDirectory();

            sort.GetFileAPIList();
            sort.Login();
            var files = sort.GetVideoFileList().Data.SynologyFiles.FindAll(x => x.FileName.Contains("Middle"));

            var directory = files.FindAll(x => x.IsDirectory).OrderBy(y => y.FileFullPath.Length).First().FileFullPath;
            //var dir = directory.

            sort.MoveFiles(files.FindAll(x => !x.IsDirectory), directory);
            // Check sid is applied
        }

        [Test]
        public void when_search_for_filename_no_error_is_returned()
        {
            SynologyDirectory synologyDirectory = new SynologyDirectory();

            synologyDirectory.GetFileAPIList();
            synologyDirectory.Login();

            synologyDirectory.GetFiles("*Middle*");

            //Assert.DoesNotThrow((x) => synologyDirectory.GetVideoFileList());
        }

    }
}
