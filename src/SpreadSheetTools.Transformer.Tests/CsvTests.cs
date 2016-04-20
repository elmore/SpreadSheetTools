using System;
using NUnit.Framework;

namespace Erm.Reporting.Transformer.Tests
{
    [TestFixture]
    public class CsvTests
    {
        [Test]
        public void CsvLoadsString()
        {
            var csv = Csv.Parse("1,2,3,4");

            Assert.AreEqual(4, csv.Length);
        }

        [Test]
        public void CsvLoadsMultiRowString()
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
        public void CsvIndexesByExcelStyleIndex(string coord, string val)
        {
            var s = string.Format("1,2,3,4{0}5,6,7,8{0}9,10,11,12", Environment.NewLine);

            var csv = Csv.Parse(s);

            Assert.AreEqual(val, csv.Get(coord));
        }

        [Test]
        public void CsvShouldSumRange()
        {
            var s = string.Format("1,2,3,4{0}5,6,7,8{0}9,10,11,12", Environment.NewLine);

            var csv = Csv.Parse(s);

            int total = csv.Take("A1").SumWith("B2").Calculate();

            Assert.AreEqual(7, total);
        }
    }
}
