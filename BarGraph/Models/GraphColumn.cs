using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BarGraph.Models
{
    public class GraphColumn
    {
        public string Name { get; set; }
        public System.Drawing.Color Color { get; set; }
        public int Value { get; set; }
        public int LineNumber { get; set; }
        
        public Exception Error { get; set; }
        public bool IsValid { get { return Error == null; } }

        public GraphColumn()
        {

        }

        public GraphColumn(string name, Color color, int value, int lineNumber)
        {
            this.LineNumber = lineNumber;
            this.Name = name;
            this.Color = color;
            this.Value = value;
        }
    }
}