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
        public Point Location { get { return new Point(X, Y); } }
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Value { get; set; }

        public Cell(char c, int x, int y)
        {
            Value = c;
            X = x;
            Y = y;
        }
    }
}
