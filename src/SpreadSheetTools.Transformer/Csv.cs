using System;

namespace Erm.Reporting.Transformer
{
    public class Csv
    {
        private readonly string[,] _table;

        private Csv(string[,] table)
        {
            _table = table;
        }

        public static Csv Parse(string csvStr)
        {
            string[] rows = csvStr.Split(new [] { Environment.NewLine }, StringSplitOptions.None);

            string[] firstRow = rows[0].Split(',');

            var t = new string[rows.Length, firstRow.Length];

            for (int i=0; i<rows.Length; i++)
            {
                string[] cells = rows[i].Split(',');

                for (int j=0; j<cells.Length; j++)
                {
                    t[i, j] = cells[j];
                }
            }

            return new Csv(t);
        }

        public int Length
        {
            get { return _table.Length; }
        }



        public string Get(string indexStr)
        {
            Tuple<int, int> coord = IndexParser.Parse(indexStr);

            return _table[coord.Item1, coord.Item2];
        }

        public Csv Take(string indexStr)
        {
            //Get(indexStr).

            throw new NotImplementedException();
        }

        public Csv SumWith(string indexStr)
        {
            throw new NotImplementedException();
        }

        public int Calculate()
        {
            throw new NotImplementedException();
        }

    }
}
