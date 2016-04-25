using System.Collections.Generic;

namespace SpreadSheetTools.Transformer
{
    /// <summary>
    /// wraps a calcution class to curry up the data
    /// </summary>
    public class LoadedCalculation
    {
	    private readonly Dictionary<string, int> _data;
        private AtlasGridCalculation _calc;

        public LoadedCalculation(Dictionary<string, int> data)
	    {
		    _data = data;
	    }
	
	    public int Eval()
	    {	
		    return _calc.Eval(_data);
	    }
	
	    public LoadedCalculation Value(string key)
	    {
            _calc = AtlasGridCalculation.Value(key);
		
		    return this;
	    }
	
	    public LoadedCalculation Sum(string key)
	    {
		    _calc = _calc.Sum(key);
		
		    return this;
	    }
	
	    public LoadedCalculation Sub(string key)
	    {
		    _calc = _calc.Sub(key);
		
		    return this;
	    }
	
	    public LoadedCalculation Multi(string key)
	    {
		    _calc = _calc.Multi(key);
		
		    return this;
	    }
    }
}
