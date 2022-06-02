using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace rdownload
{
    class Config
    {
        // if any command line argument is passed to the program
        // then the process will ommit the questions and start to download all files automatically
        // (good for running the program via schedule in the background)
        public static string configXmlFilePath = "rfeeds.xml";
        public static string outputDirectory = @".\";

        // to keep the list of feeds (uri links to xml files with content)
        public static List<string> feeds = new List<string>();

        // contains title of image, and corresponding actual filename download link
        // these will be used together while downloading and file saving
        public static Dictionary<string, string> images = new Dictionary<string, string>();


        // process main command line arguments and sets proper variables and validity flag

        public static bool noValidMainArguments = true;
        public static void ValidateAndSetMainArguments(string[] args)
        {
            if (args.Length == 2)
            {
                configXmlFilePath = args[0];
                outputDirectory = args[1];
                noValidMainArguments = false;
            }
        }




        public static void ProcessConfigFile()
        {
            try
            {
                XElement xelement = XElement.Load(configXmlFilePath);
                IEnumerable<XElement> feeds = xelement.Elements();
                // Read the entire XML
                foreach (var feed in feeds)
                    Config.feeds.Add(feed.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ShowMissingConfigFileMsg();
                Menu.DisplayPressAnyKeyMsg();
                System.Environment.Exit(1);
            }

        }


        // display info with the sample content of missing xml config file
        static void ShowMissingConfigFileMsg()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nCreate 'rfeeds.xml' file in the program directory with the following content:\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("<?xml version=\"1.0\"?>");
            Console.WriteLine("<feeds>");
            Console.WriteLine("\t<feed>https://www.reddit.com/r/EarthPorn/.xml</feed>");
            Console.WriteLine("\t<feed>https://www.reddit.com/r/beautiful_world/.xml</feed>");
            Console.WriteLine("</feeds>");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nor run program with two arguments:\n");
            Console.ResetColor();
            Console.WriteLine("rdownload <path to config file> <output directory for saving files>");
            Console.WriteLine("rdownload rfeeds.xml .");
            Console.WriteLine(@"rdownload C:\files\rfeeds.xml C:\images\");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
