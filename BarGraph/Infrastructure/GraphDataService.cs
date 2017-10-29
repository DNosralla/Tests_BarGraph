using BarGraph.Interfaces;
using BarGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarGraph.Infrastructure
{
    public class GraphDataService : IGraphDataService
    {
        private const int MAX_VALUE = 10;
        private IEnumerable<GraphColumn> columns = new List<GraphColumn>();

        public void SetInitialValues(IEnumerable<GraphColumn> columns) {
            this.columns = columns;
        }

        public IEnumerable<GraphColumn> RandomizeValues()
        {
            var rnd = new Random();
            int newValue;
            
            foreach (var col in columns)
            {
                //make shure the new random value is not equal the previous
                do
                {
                    newValue = rnd.Next(0, MAX_VALUE);
                } while (newValue == col.Value);

                col.Value = newValue;
            }

            return columns;
        }
    }
}