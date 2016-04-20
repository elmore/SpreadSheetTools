
namespace SpreadSheetTools.Transformer
{
    public abstract class Calculation
    {
        private readonly int _val;
        private readonly Calculation _parent;

        protected Calculation(int val, Calculation chain = null)
        {
            _val = val;
            _parent = chain;
        }

        public static Unit Value(int val)
        {
            return new Unit(val, null);
        }

        public Summation Sum(int val)
        {
            return new Summation(val, this);
        }

        public Subtraction Sub(int val)
        {
            return new Subtraction(val, this);
        }

        public Multiplication Multi(int val)
        {
            return new Multiplication(val, this);
        }

        public int Eval()
        {
            int parentVal = _parent != null ? _parent.Eval() : 0;

            return Combine(_val, parentVal);
        }

        public abstract int Combine(int input1, int input2);

        public class Unit : Calculation
        {
            public Unit(int val, Calculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input1;
            }
        }

        public class Summation : Calculation
        {
            public Summation(int val, Calculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input1 + input2;
            }
        }

        public class Subtraction : Calculation
        {
            public Subtraction(int val, Calculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input2 - input1;
            }
        }

        public class Multiplication : Calculation
        {
            public Multiplication(int val, Calculation chain)
                : base(val, chain) { }

            public override int Combine(int input1, int input2)
            {
                return input1 * input2;
            }
        }
    }
}
