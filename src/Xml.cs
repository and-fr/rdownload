using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace rdownload
{
    class Xml
    {

        // process xml file (feed) content to retrieve image file nams and titles
        public static void ProcessFeedFile(string uri)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(uri);
            Console.WriteLine();
            Console.ResetColor();

            // prepare root of xml file
            string xml = uri;
            XElement xelement = XElement.Load(xml);
            IEnumerable<XElement> feed = xelement.Elements();

            // create a list, parse whole xml document
            // and add only html data which contains title and image link

            List<string> html = new List<string>();
            foreach (var entry in feed)
            {
                if (entry.Name.LocalName == "entry")
                {
                    foreach (var item in entry.Elements())
                    {
                        if (item.Name.LocalName == "content")
                            html.Add(item.Value);
                    }
                }
            }

            // extract titles, and links and put them in dictionary

            Config.images.Clear();

            string title = "";
            string link = "";

            foreach (string item in html)
            {
                Match matchTitle = Regex.Match(item, "title=\"([^\"]*)\"");
                if (matchTitle.Success)
                    title = Text.SanitizeTitle(matchTitle.Captures[0].Value);

                Match matchLink = Regex.Match(item, "https://i.*jpg");
                if (matchLink.Success)
                    link = matchLink.Captures[0].Value;

                if ((matchTitle.Success) && (matchLink.Success))
                {
                    try
                    {
                        Config.images.Add(title, link);
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
}
