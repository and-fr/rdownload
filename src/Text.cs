using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rdownload
{
    class Text
    {
        static List<int> suffixNumbers = new List<int>();

        public static string SanitizeTitle(string title)
        {
            // randomized suffix added to prevent the same title used more than once
            Random r = new Random();
            int suffixNum = 1000;
            while (suffixNumbers.Contains(suffixNum))
                suffixNum = r.Next(1000, 9999);
            suffixNumbers.Add(suffixNum);

            title = title.Replace("title=", "");
            title = title.Replace('"', ' ').Trim();

            // delete any brakets and any text within
            title = Regex.Replace(title, "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))", "");

            // delete all but letters
            title = Regex.Replace(title, "[^a-zA-Z0-9 ]", "");

            title = title.Trim();
            title = title.Replace(" ", "_");
            title = title.ToLower();
            title = title + "_" + suffixNum.ToString();

            return title;
        }
    }
}
