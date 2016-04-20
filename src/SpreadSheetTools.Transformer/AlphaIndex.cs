using System;

namespace Erm.Reporting.Transformer
{
    public class AlphaIndex
    {
        private readonly int _val;

        private AlphaIndex(int val)
        {
            _val = val;
        }

        public int ToInt32()
        {
            return _val;
        }

        public static bool TryParse(string s, out AlphaIndex alphaIndex)
        {
            Char c;
            if (!Char.TryParse(s, out c))
            {
                alphaIndex = null;

                return false;
            }

            int i = char.ToUpper(c) - 64;

            alphaIndex = new AlphaIndex(i);

            return true;
        }
    }
}