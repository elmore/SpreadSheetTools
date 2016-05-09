using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpreadSheetTools.Transformer
{
    public class Sheet
    {
        private Dictionary<string, int> _data;
        private readonly Dictionary<string, AtlasGridCalculation> _calcs = new Dictionary<string, AtlasGridCalculation>();

        public void Define(string dest, string calcStr)
        {
            //var re = new Regex(@"AVERAGE\(([a-z]+\d+):([a-z]+\d+)\)", RegexOptions.IgnoreCase);
            var re = new Regex(@"SUM\(([a-z]+\d+):([a-z]+\d+)\)", RegexOptions.IgnoreCase);

            var m = re.Match(calcStr);

            string start = m.Groups[1].Value;
            string end = m.Groups[2].Value;

            Tuple<int, int> indexStart = IndexParser.Parse(start);
            Tuple<int, int> indexEnd = IndexParser.Parse(end);

            AtlasGridCalculation calc = AtlasGridCalculation.Value(start);

            // + 1 because we have added the first value already above..
            for (var i = indexStart.Item2 + 1; i <= indexEnd.Item2; i++)
            {
                string grid = IndexParser.Generate(indexStart.Item1, i);

                calc = calc.Sum(grid);
            }

            _calcs.Add(dest, calc);
        }

        public void Load(Dictionary<string, int> data)
        {
            _data = data;
        }

        public int Get(string coord)
        {
            AtlasGridCalculation calc;
            if (_calcs.TryGetValue(coord, out calc))
            {
                _data[coord] = calc.Eval(_data);
            }

            return _data[coord];
        }
    }
}
