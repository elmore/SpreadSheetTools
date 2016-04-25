using System;

namespace SpreadSheetTools.Transformer
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

        public override string ToString()
        {
            var utf = _val + 64;

            var ch = (char)utf;

            return ch.ToString();
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

        public static bool TryParse(int i, out AlphaIndex alphaIndex)
        {
            if (i < 0 || i > 64)
            {
                alphaIndex = null;

                return false;
            }

            alphaIndex = new AlphaIndex(i);

            return true;
        }
    }
}