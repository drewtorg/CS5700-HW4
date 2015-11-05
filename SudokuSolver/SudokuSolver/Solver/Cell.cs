using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudokuSolver.Solver
{
    public class Cell
    {
        public Point Location { get { return new Point(Row, Column); } }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public char Value { get; set; }

        public Cell(char val, int r, int c)
        {
            Value = val;
            Row = r;
            Column = c;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            return Value == other.Value && Row == other.Row && Column == other.Column;
        }
    }
}
