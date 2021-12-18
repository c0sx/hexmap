using System.Collections.Generic;

namespace Grid.Selector
{
    public class Borders
    {
        private int _width;
        private int _height;

        public Borders(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public bool Includes(int rowIndex, int cellIndex)
        {
            var start = rowIndex * _width;
            var end = start + _width - 1;

            return cellIndex >= start && cellIndex <= end;
        }

        public List<int> IncludesIndexes(int rowIndex, List<int> indexes) 
        {
            return indexes.FindAll(index => Includes(rowIndex, index));
        }
    }
}

