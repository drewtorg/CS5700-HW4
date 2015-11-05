using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class OneLeftSolver : Solver
    {
        public override bool SolveCell(Puzzle puzzle, int r, int c)
        {
            return FindLastOne(puzzle, r, c, puzzle.BoxAt(r, c)) ||
                   FindLastOne(puzzle, r, c, puzzle.ColumnAt(c)) ||
                   FindLastOne(puzzle, r, c, puzzle.RowAt(r));
        }

        private bool FindLastOne(Puzzle puzzle, int r, int c, Cell[] searchMe)
        {
            var numDashes = searchMe.Where(cell => cell.Value == '-')
                                    .Count();
            if (numDashes == 1)
            {
                puzzle.Cells[r, c].Value = puzzle.CharacterSet.Except( searchMe.Select(cell => cell.Value) )
                                                              .Single();
                return true;
            }

            return false;
        }
    }
}
