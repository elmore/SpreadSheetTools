using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpreadSheetTools.Transformer
{
    public class Sheet
    {
        private Dictionary<string, int> _data;
        private readonly Dictionary<string, ICalculation> _calcs = new Dictionary<string, ICalculation>();

        public void Define(string dest, string calcStr)
        {
            // pull out into command pattern

            var re = new Regex(@"(SUM|AVERAGE)\(([a-z]+\d+):([a-z]+\d+)\)", RegexOptions.IgnoreCase);

            var m = re.Match(calcStr);

            string command = m.Groups[1].Value;
            string start = m.Groups[2].Value;
            string end = m.Groups[3].Value;

            Tuple<int, int> indexStart = IndexParser.Parse(start);
            Tuple<int, int> indexEnd = IndexParser.Parse(end);

            ICalculation calc = AtlasGridCalculation.Value(start);

            // + 1 because we have added the first value already above..
            for (var i = indexStart.Item2 + 1; i <= indexEnd.Item2; i++)
            {
                string grid = IndexParser.Generate(indexStart.Item1, i);

                // blah
                calc = ((AtlasGridCalculation)calc).Sum(grid);
            }

            if (command == "AVERAGE")
            {
                calc = new Average(calc, indexEnd.Item2 - indexStart.Item2);
            }

            _calcs.Add(dest, calc);
        }

        public void Load(Dictionary<string, int> data)
        {
            _data = data;
        }

        public int Get(string coord)
        {
            ICalculation calc;
            if (_calcs.TryGetValue(coord, out calc))
            {
                _data[coord] = calc.Eval(_data);
            }

            return _data[coord];
        }
    }
}
