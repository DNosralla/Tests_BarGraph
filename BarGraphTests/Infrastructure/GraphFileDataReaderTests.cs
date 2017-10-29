using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarGraph.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarGraph.Infrastructure.Tests
{
    [TestClass()]
    public class GraphFileDataReaderTests
    {
        [TestMethod()]
        public void GetLinesFromFile()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = System.IO.Path.Combine(baseDir, "GraphFile", "barsInputFile.txt");
            Assert.IsTrue(File.Exists(filePath));

            var file = new FileInfo(filePath);

            var reader = new GraphFileDataReader();

            using (var stream = file.OpenRead())
            {
                var lines = reader.GetLinesFromFile(stream);

                Assert.IsNotNull(lines);
                Assert.AreEqual(2, lines.Count());

                Assert.AreEqual(lines.ElementAt(0), "#A:RED:5");
                Assert.AreEqual(lines.ElementAt(1), "#B:BLUE:10");
            }
        }

        [TestMethod()]
        public void GetColumnsTest_Valid()
        {
            var lines = new List<string>() {
                "#A:RED:5",
                "#B:BLUE:10"
            };

            var reader = new GraphFileDataReader();
            var columns = reader.GetColumns(lines);

            Assert.IsNotNull(columns);
            Assert.AreEqual(2, columns.Count());
            Assert.IsTrue(columns.All(c => c.IsValid));
            Assert.IsFalse(columns.Any(c => c.Error != null));
        }

        [TestMethod()]
        public void GetColumnsTest_Name_Missing_Hashtag()
        {
            var lines = new List<string>() {
                "A:RED:5"
            };

            var reader = new GraphFileDataReader();
            var columns = reader.GetColumns(lines);

            Assert.IsNotNull(columns);
            Assert.AreEqual(1, columns.Count());
            Assert.IsFalse(columns.ElementAt(0).IsValid);
            Assert.IsInstanceOfType(columns.ElementAt(0).Error, typeof(GraphFileDataReaderException));
        }

        [TestMethod()]
        public void GetColumnsTest_Name_InvalidCharacters()
        {
            var lines = new List<string>() {
                "#A!@$%¨&*:RED:5"
            };

            var reader = new GraphFileDataReader();
            var columns = reader.GetColumns(lines);

            Assert.IsNotNull(columns);
            Assert.AreEqual(1, columns.Count());
            Assert.IsFalse(columns.ElementAt(0).IsValid);
            Assert.IsInstanceOfType(columns.ElementAt(0).Error, typeof(GraphFileDataReaderException));
        }

    }
}