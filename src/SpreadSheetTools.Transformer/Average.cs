using System.Collections.Generic;

namespace SpreadSheetTools.Transformer
{
    /// <summary>
    ///  would rather this was part of the 'monad-y' pattern but it has a different api (count) 
    /// so i need to think about how it will be combined with other things. b-tree perhaps
    /// </summary>
    public class Average : ICalculation
    {
        private readonly ICalculation _calc;
        private readonly int _count;

        public Average(ICalculation calc, int count)
        {
            _calc = calc;
            _count = count;
        }

        public int Eval(Dictionary<string, int> data)
        {
            return _calc.Eval(data)/_count;
        }
    }
}