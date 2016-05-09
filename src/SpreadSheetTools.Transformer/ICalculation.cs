using System.Collections.Generic;

namespace SpreadSheetTools.Transformer
{
    public interface ICalculation
    {
        int Eval(Dictionary<string, int> data);
    }
}