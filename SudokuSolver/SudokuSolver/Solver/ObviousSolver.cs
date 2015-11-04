using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class ObviousSolver : Solver
    {
        public override bool SolveCell(Puzzle puzzle, int r, int c)
        {
            var matches = puzzle.RowAt(r).Where(cell => cell.Value == '-').ToList();
            if(matches.Count == 1)
            {
                puzzle.Cells[r, c].Value = puzzle.CharacterSet.Except(puzzle.RowAt(r)
                                                              .Select(cell => cell.Value))
                                                              .Single();
                return true;
            }
            
            matches = puzzle.ColumnAt(c).Where(cell => cell.Value == '-').ToList();
            if (matches.Count == 1)
            {
                puzzle.Cells[r, c].Value = puzzle.CharacterSet.Except(puzzle.ColumnAt(c)
                                                              .Select(cell => cell.Value))
                                                              .Single();
                return true;
            }

            matches = puzzle.BoxAt(r, c).Where(cell => cell.Value == '-').ToList();
            if (matches.Count == 1)
            {
                puzzle.Cells[r, c].Value = puzzle.CharacterSet.Except(puzzle.BoxAt(r, c)
                                                              .Select(cell => cell.Value))
                                                              .Single();
                return true;
            }
            return false;
        }
    }
}
