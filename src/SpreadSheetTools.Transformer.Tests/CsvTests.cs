using System;
using NUnit.Framework;

namespace SpreadSheetTools.Transformer.Tests
{
    [TestFixture]
    public class CsvTests
    {
        [Test]
        public void LoadsString()
        {
            var csv = Csv.Parse("1,2,3,4");

            Assert.AreEqual(4, csv.Length);
        }

        [Test]
        public void LoadsMultiRowString()
        {
            var s = string.Format("1,2,3,4{0}5,6,7,8{0}9,10,11,12", Environment.NewLine);

            var csv = Csv.Parse(s);

            Assert.AreEqual(12, csv.Length);
        }

        [TestCase("A1", "1")]
        [TestCase("A2", "2")]
        [TestCase("B3", "7")]
        [TestCase("B4", "8")]
        [TestCase("C4", "12")]
        [TestCase("C1", "9")]
        public void IndexesByExcelStyleIndex(string coord, string val)
        {
            var s = string.Format("1,2,3,4{0}5,6,7,8{0}9,10,11,12", Environment.NewLine);

            var csv = Csv.Parse(s);

            Assert.AreEqual(val, csv.Get(coord));
        }

        [Test]
        public void ShouldRespectOrderOfOperations()
        {
            var s = string.Format("1,2,3,4{0}5,6,7,8{0}9,10,11,12", Environment.NewLine);

            var csv = Csv.Parse(s);

            // 3 + 6 * 2
            int total = csv.Take("A3").Sum("B2").Multi("A2").Eval();

            Assert.AreEqual(15, total);
        }
    }
}
