using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SpreadSheetTools.Transformer.Tests
{
    [TestFixture]
    public class SheetTests
    {
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

            sheet.Define("A1", "AVERAGE(A5:A10)");

            sheet.Load(data);

            int val = sheet.Get("A1");

            Assert.AreEqual(3, val);
        }


    }
}
