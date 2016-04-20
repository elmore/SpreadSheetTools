using System.Collections.Generic;

namespace SpreadSheetTools.Transformer
{
    /// <summary>
    /// version of Calculation that uses the 'atlas grid' coordinates that excel uses.
    /// the Eval method gets handed the data to run the calculation on:
    /// 
    /// 	var delayed = a.Sum("a1").Sum("a2").Sub("b6").Multi("c9");
    ///
    ///     delayed.Eval(csv).Dump();
    ///     
    ///     csv["b6"] = 4;
    ///     
    ///     delayed.Eval(csv).Dump();
    /// 
    /// because its not evaluated until Eval() is called, the chain encapsulates
    /// the creation of an object which can mutate data in a consistent way
    /// 
    /// </summary>
    public abstract class AtlasGridCalculation
    {
        private readonly string _key;
        private readonly AtlasGridCalculation _chained;

        protected AtlasGridCalculation(string key, AtlasGridCalculation chain = null)
        {
            _key = key;
            _chained = chain;
        }

        public static Unit Value(string key)
        {
            return new Unit(key, null);
        }

        public Summation Sum(string key)
        {
            return new Summation(key, this);
        }

        public Subtraction Sub(string key)
        {
            return new Subtraction(key, this);
        }

        public Multiplication Multi(string key)
        {
            return new Multiplication(key, this);
        }

        public int Eval(Dictionary<string, int> data)
        {
            int parentVal = _chained != null ? _chained.Eval(data) : 0;

            int thisVal = data[_key];

            return Combine(thisVal, parentVal);
        }

        public abstract int Combine(int input1, int input2);

        public class Unit : AtlasGridCalculation
        {
            public Unit(string val, AtlasGridCalculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input1;
            }
        }

        public class Summation : AtlasGridCalculation
        {
            public Summation(string val, AtlasGridCalculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input1 + input2;
            }
        }

        public class Subtraction : AtlasGridCalculation
        {
            public Subtraction(string val, AtlasGridCalculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input2 - input1;
            }
        }

        public class Multiplication : AtlasGridCalculation
        {
            public Multiplication(string val, AtlasGridCalculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input2 * input1;
            }
        }
    }
}
