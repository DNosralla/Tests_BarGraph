using System.Collections.Generic;
using BarGraph.Models;

namespace BarGraph.Interfaces
{
    public interface IGraphDataService
    {
        IEnumerable<GraphColumn> RandomizeValues();
        void SetInitialValues(IEnumerable<GraphColumn> columns);
    }
}