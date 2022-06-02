using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

// Reddit image downloader

namespace rdownload
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.ValidateAndSetMainArguments(args);
            Config.ProcessConfigFile();
            Menu.DisplayWelcomeMsg();
            Menu.DisplayFeedsToDownloadFrom();
            
            if (Config.noValidMainArguments) Menu.AskForDownload();
            
            Console.WriteLine("Downloading...\n");
            foreach(string feed in Config.feeds)
            {
                Xml.ProcessFeedFile(feed);
                //Menu.DisplayDictionary();
                Download.RetrieveImages();
            }

            if (Config.noValidMainArguments) Menu.DisplayPressAnyKeyMsg();

        }
    }
}
