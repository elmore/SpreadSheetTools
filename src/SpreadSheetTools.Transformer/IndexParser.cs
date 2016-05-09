using System;
using System.Text.RegularExpressions;

namespace SpreadSheetTools.Transformer
{
    public static class IndexParser
    {
        public static Tuple<int, int> Parse(string indexStr)
        {
            var splitter = new Regex(@"([a-z]+)(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            Match m = splitter.Match(indexStr);

            string xStr = m.Groups[1].Value;

            string yStr = m.Groups[2].Value;

            AlphaIndex x;
            if (!AlphaIndex.TryParse(xStr, out x))
            {
                throw new ArgumentException(string.Format("The string '{0}' could not be parsed into x,y coordinates", indexStr));
            }

            int y;
            if (!Int32.TryParse(yStr, out y))
            {
                throw new ArgumentException(string.Format("The string '{0}' could not be parsed into x,y coordinates", indexStr));
            }

            // - 1 because zero indexed api
            return new Tuple<int, int>(x.ToInt32() - 1, y - 1);
        }

        public static string Generate(int x, int y)
        {            
            AlphaIndex xindex;

            // + 1 because zero indexed api
            if(!AlphaIndex.TryParse(x + 1, out xindex))
            {
                throw new ArgumentException(string.Format("The int '{0}' could not be parsed into x,y coordinates", x));
            }

            // + 1 because zero indexed api
            return string.Format("{0}{1}", xindex, y + 1);

        }
    }
}