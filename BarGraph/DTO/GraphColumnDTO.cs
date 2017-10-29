using BarGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarGraph.DTO
{
    public class GraphColumnDTO
    {
        public string name { get; set; }

        public string color { get; set; }
        public string colorHex { get; set; }

        public int value { get; set; }
        public int lineNumber { get; set; }

        public string error { get; set; }
        public bool isValid { get; set; }

        public GraphColumnDTO()
        {

        }

        public GraphColumnDTO(GraphColumn col)
        {
            this.name = col.Name;

            this.color = col.Color.Name;
            this.colorHex = String.Format("#{0:X6}", col.Color.ToArgb() & 0x00FFFFFF);

            this.value = col.Value;
            this.lineNumber = col.LineNumber;
            this.error = col.Error?.Message;
            this.isValid = col.IsValid;
        }

        
    }
}