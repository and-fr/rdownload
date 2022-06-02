using System;
using System.Collections.Generic;

namespace rdownload
{
    class Menu
    {
        public static void DisplayWelcomeMsg()
        {
            const string APP_NAME = "REDDIT feeds image downloader";
            const string APP_VER = " v0.2.2020.02.10";
            const string APP_INFO = 
                "you can also run this program non-ineteractively:\n\n" +
                "rdownload <path to config file> <output directory>\n" +
                "rdownload rfeeds.xml .\n" +
                @"rdownload c:\feeds\r.xml c:\images";

            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(APP_NAME);
            Console.ForegroundColor = ConsoleColor.Gray;   Console.Write(APP_VER);
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(APP_INFO);
            Console.WriteLine();
        }


        public static void DisplayFeedsToDownloadFrom()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("This tool can download images (jpg files) from following feeds:");
            Console.WriteLine();
            Console.ResetColor();
            foreach (string feed in Config.feeds)
                Console.WriteLine(feed);
            Console.WriteLine();
        }


        public static void AskForDownload()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Proceed with the download? [y/n]");

            bool ask = true;
            char button;
            while (ask)
            {
                button = Console.ReadKey(true).KeyChar;
                switch (button)
                {
                    case 'y': ask = false; break;
                    default:
                        Console.ResetColor();
                        System.Environment.Exit(1);
                        break;
                }
            }

            Console.WriteLine();
            Console.ResetColor();
        }



        public static void DisplayPressAnyKeyMsg()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to quit.");
            Console.ResetColor();
            Console.ReadKey(true);
        }



        public static void DisplayDictionary()
        {
            foreach (KeyValuePair<string, string> image in Config.images)
            {
                Console.WriteLine($"{image.Key}\n{image.Value}\n");
            }
        }
    }
}
