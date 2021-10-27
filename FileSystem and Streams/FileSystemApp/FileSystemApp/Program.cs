using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FileSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // get MyDocuments path
            var newDirPath = Path.Combine(myDocumentsPath, "Downloads"); // create Downloads folder

            if(!Directory.Exists(newDirPath)) // check if the directory exists 
                Directory.CreateDirectory(newDirPath); // if it doesn't exist, create it

            var downloadsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\");

            DirectoryInfo directoryInfo = new DirectoryInfo(downloadsDir); // create a directoryInfo for Downloads

            var files = directoryInfo.GetFiles(); // get all files from Downloads

            var filtered_files = files.GroupBy(file => file.CreationTime.Date); // group files by date


            foreach (var groups in filtered_files) // iterate over filtered files
                foreach (var file in groups) // iterate over groups 
                    SendFile.sendFile(file, $"{file.CreationTime.Date.Day}.{file.CreationTime.Date.Month}.{file.CreationTime.Date.Year}", newDirPath); // change the file path
               
        }
    }
}
