using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public abstract class Solver
    {
        public abstract bool SolveCell(Puzzle puzzle, int r, int c);

        public bool IsSolved(Puzzle puzzle)
        {
            for (int i = 0; i < puzzle.TotalRows; i++)
                for (int j = 0; j < puzzle.TotalRows; j++)
                    if (puzzle.Cells[i, j].Value == '-')
                        return false;
            return true;
        }

        public bool SolvePuzzle(Puzzle puzzle)
        {
            bool tryAgain = true;
            
            while (tryAgain)
            {
                tryAgain = false;
                for (int i = 0; i < puzzle.TotalRows; i++)
                    for (int j = 0; j < puzzle.TotalRows; j++)
                        if (puzzle.Cells[i, j].Value == '-')
                            tryAgain = SolveCell(puzzle, i, j) ? true : tryAgain;
            }
            return IsSolved(puzzle);
        }
    }
}
