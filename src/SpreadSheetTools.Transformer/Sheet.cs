using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpreadSheetTools.Transformer
{
    public class Sheet
    {
        private Dictionary<string, int> _data;
        private AtlasGridCalculation _calc;

        public void Define(string dest, string calcStr)
        {
            Tuple<int, int> index = IndexParser.Parse(dest);

            var re = new Regex(@"AVERAGE\(([a-z]+\d+):([a-z]+\d+)\)", RegexOptions.IgnoreCase);

            var m = re.Match(calcStr);

            string start = m.Groups[1].Value;
            string end = m.Groups[2].Value;

            Tuple<int, int> indexStart = IndexParser.Parse(start);
            Tuple<int, int> indexEnd = IndexParser.Parse(end);

            AtlasGridCalculation calc = AtlasGridCalculation.Value(start);

            for (var i = indexStart.Item2+1; i < indexEnd.Item2; i++)
            {
                string grid = IndexParser.Generate(indexStart.Item1, i);

                calc = calc.Sum(grid);
            }

            _calc = calc;
        }

        public void Load(Dictionary<string, int> data)
        {
            _data = data;
        }

        public int Get(string coord)
        {
            return _calc.Eval(_data);
        }
    }
}
