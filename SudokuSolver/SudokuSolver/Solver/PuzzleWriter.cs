using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuSolver.Solver
{
    public static class PuzzleWriter
    {
        public static void WritePuzzle(string filename, Puzzle puzzle)
        {
            if (filename != null && puzzle != null)
                File.WriteAllText(filename, puzzle.ToString());
        }
    }
}
