using System.Collections.Generic;
using NUnit.Framework;

namespace SpreadSheetTools.Transformer.Tests
{
    [TestFixture]
    public class SheetTests
    {
        [Test]
        public void CanSelectValue()
        {
            var data = new Dictionary<string, int>
            {
                { "A1", 0 },
                { "A5", 1 },
                { "A6", 2 },
                { "A7", 3 },
                { "A8", 4 },
                { "A9", 5 },
                { "A10", 6 },
            };

            var sheet = new Sheet();

            sheet.Load(data);

            int val = sheet.Get("A5");

            Assert.AreEqual(1, val);
        }


        [Test]
        public void CanDefineRange()
        {
            var data = new Dictionary<string, int>
            {
                { "A1", 0 },
                { "A5", 1 },
                { "A6", 2 },
                { "A7", 3 },
                { "A8", 4 },
                { "A9", 5 },
                { "A10", 6 },
            };

            var sheet = new Sheet();

            sheet.Define("A1", "SUM(A5:A10)");

            sheet.Load(data);

            int val = sheet.Get("A1");

            Assert.AreEqual(21, val);
        }


    }
}
