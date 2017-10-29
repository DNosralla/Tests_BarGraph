using BarGraph.Interfaces;
using BarGraph.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BarGraph.Infrastructure
{
    public class GraphFileDataReader : IGraphFileDataReader
    {
        private const char COL_SEPARATOR = ':';
        private const string NAME_REGEX_EXPRESSION = "^#[a-zA-Z0-9]+$";

        public IEnumerable<string> GetLinesFromFile(Stream fileStream)
        {
            var lines = new List<string>();

            using (var reader = new System.IO.StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            
            return lines;
        }
        
        public IEnumerable<GraphColumn> GetColumns(IEnumerable<string> lines)
        {
            var result = new List<GraphColumn>();

            for (int i = 0; i < lines.Count(); i++)
            {
                try
                {
                    result.Add(GetColumnFromFileLine(lines.ElementAt(i), i));
                }
                catch (Exception e)
                {
                    result.Add(new GraphColumn()
                    {
                        Error = e,
                        LineNumber = i
                    });
                }
            }

            return result;
        }

        private GraphColumn GetColumnFromFileLine(string line, int lineIndex)
        {
            var lineCols = line.Split(COL_SEPARATOR);

            if (lineCols.Length != 3)
            {
                throw new GraphFileDataReaderException($"Invalid format, expected 3 columns, got {lineCols.Length}.");
            }

            
            #region name
            string name;
            if (!IsValidColumnName(lineCols[0]))
            {
                throw new GraphFileDataReaderException($"Invalid Name: {lineCols[0]}");
            }
            else
            {
                //remove leading '#' char
                name = lineCols[0].Remove(0, 1);
            }
            #endregion

            #region Color
            Color color;
            if (String.IsNullOrWhiteSpace(lineCols[1]))
            {
                throw new GraphFileDataReaderException($"Invalid Color: {lineCols[1]}");
            } else
            {
                color = Color.FromName(lineCols[1]);
            }
            #endregion

            #region Value
            int value;
            if (String.IsNullOrWhiteSpace(lineCols[2]) || !int.TryParse(lineCols[2], out value))
            {
                throw new GraphFileDataReaderException($"Invalid Value: {lineCols[2]}");
            }
            #endregion

            return new GraphColumn(name, color, value, lineIndex);
        }

        /// <summary>
        /// Determines if name starts with # and contains only numbers and letters
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsValidColumnName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            Regex regex = new Regex(NAME_REGEX_EXPRESSION);
            if (!regex.IsMatch(name))
            {
                return false;
            }

            return true;
        }
    }

    public class GraphFileDataReaderException : Exception
    {
        public GraphFileDataReaderException()
        {
        }

        public GraphFileDataReaderException(string message) : base(message)
        {
        }

        public GraphFileDataReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}