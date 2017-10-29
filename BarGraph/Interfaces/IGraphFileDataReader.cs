using System.Collections.Generic;
using System.IO;
using BarGraph.Models;

namespace BarGraph.Interfaces
{
    public interface IGraphFileDataReader
    {
        IEnumerable<string> GetLinesFromFile(Stream fileStream);
        IEnumerable<GraphColumn> GetColumns(IEnumerable<string> lines);
    }
}