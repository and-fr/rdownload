using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace rdownload
{
    class Download
    {
        public static void RetrieveImages()
        {
            // process dictionary
            // show titles, downloaded file names, etc.

            int count = 0;
            string filename = "";
            foreach (KeyValuePair<string, string> entry in Config.images)
            {
                filename = "r_" + entry.Key + ".jpg";
                Console.WriteLine($"{++count,3} : {filename}");

                try
                {
                    // Get back the HTTP response for web server
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(entry.Value);
                    httpRequest.Method = WebRequestMethods.Http.Get;
                    HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    Stream httpResponseStream = httpResponse.GetResponseStream();

                    // Define buffer and buffer size
                    int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead = 0;

                    // Read from response and write to file
                    FileStream fileStream = File.Create(Config.outputDirectory + filename);
                    while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: {0}", ex.Message);
                    Console.ResetColor();
                }

            }

        }
    }
}
