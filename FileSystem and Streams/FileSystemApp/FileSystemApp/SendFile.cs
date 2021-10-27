using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystemApp
{
    public static class SendFile
    {
        public static void sendFile(FileInfo file, string dirName, string newDirPath)
        {
            using FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read); // init the stream
            byte[] bytes = new byte[stream.Length]; // create a byte array
            int bytesToRead = (int)stream.Length; // get the length of the stream
            int bytesRead = 0; 

            while(bytesToRead > 0)
            {
                int n = stream.Read(bytes, bytesRead, bytesToRead); // read the bytes

                if (n == 0) // end of the file
                    break;

                bytesRead += n;
                bytesToRead -= n;
            }
            bytesToRead = bytes.Length;

            stream.Close();

            File.Delete(file.FullName);


            var new_path = Path.Combine(newDirPath, dirName);

            if (!Directory.Exists(new_path)) // check if the "date dir" exists, if it doesn't exist, create it
                Directory.CreateDirectory(new_path); // create the new dir

            var movedFile = Path.Combine(new_path, file.Name); // create the file path

            using FileStream sw = new FileStream(movedFile, FileMode.Create, FileAccess.Write); // init the stream writer 
            sw.Write(bytes, 0, bytesToRead); // write the file
            






        }
    }
}
