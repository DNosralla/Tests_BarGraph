using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarGraph.Infrastructure;
using BarGraph.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarGraph.Infrastructure.Tests
{
    [TestClass()]
    public class GraphDataServiceTests
    {
        [TestMethod()]
        public void RandomizeValuesTest()
        {
            var service = new GraphDataService();

            var initialValues = new List<GraphColumn>() {
                new GraphColumn("A", Color.White, 0, 0),
                new GraphColumn("B", Color.Black, 1, 1)
            };

            service.SetInitialValues(initialValues);

            var randomizedResult = service.RandomizeValues();

            Assert.IsNotNull(randomizedResult);
            Assert.AreEqual(initialValues.Count, randomizedResult.Count());

            var aCol = randomizedResult.ElementAt(0);
            Assert.IsNotNull(aCol);
            Assert.AreEqual("A", aCol.Name);
            Assert.AreEqual(Color.White, aCol.Color);
            Assert.AreEqual(0, aCol.LineNumber);
            Assert.AreNotEqual(0, aCol.Value);

            var bCol = randomizedResult.ElementAt(1);
            Assert.IsNotNull(bCol);
            Assert.AreEqual("B", bCol.Name);
            Assert.AreEqual(Color.Black, bCol.Color);
            Assert.AreEqual(1, bCol.LineNumber);
            Assert.AreNotEqual(1, bCol.Value);
        }
    }
}