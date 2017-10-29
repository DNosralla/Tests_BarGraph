using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarGraph.DTO;
using BarGraph.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarGraph.DTO.Tests
{
    [TestClass()]
    public class GraphColumnDTOTests
    {
        [TestMethod()]
        public void GraphColumnDTO_Valid_Column()
        {
            var column = new BarGraph.Models.GraphColumn(
                "name",
                System.Drawing.Color.White,
                1,
                0
                );

            var dto = new DTO.GraphColumnDTO(column);

            Assert.AreEqual(column.Name, dto.name);

            Assert.AreEqual("White", dto.color);
            Assert.AreEqual("#FFFFFF", dto.colorHex);
            
            Assert.AreEqual(column.Value, dto.value);
            Assert.AreEqual(column.LineNumber, dto.lineNumber);

            Assert.IsNull(dto.error);
            Assert.IsTrue(dto.isValid);
        }

        [TestMethod()]
        public void GraphColumnDTO_InValid_Column()
        {
            var column = new BarGraph.Models.GraphColumn()
            {
                Error = new GraphFileDataReaderException("Test Exception")
            };

            var dto = new DTO.GraphColumnDTO(column);

            Assert.IsNotNull(dto.error);
            Assert.AreEqual(column.Error.Message, dto.error);
            
            Assert.IsFalse(dto.isValid);
        }
    }
}