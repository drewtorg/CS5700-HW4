using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class EliminationSolver : Solver
    {
        public override bool SolveCell(Puzzle puzzle, int r, int c)
        {
            var possibilities = new List<char>(puzzle.CharacterSet);

            var row = puzzle.RowAt(r);
            var col = puzzle.ColumnAt(c);
            var box = puzzle.BoxAt(r, c);

            for (int i = 0; i < puzzle.TotalRows; i++)
            {
                if (possibilities.Contains(row[i].Value))
                    possibilities.Remove(row[i].Value);

                if (possibilities.Contains(col[i].Value))
                    possibilities.Remove(col[i].Value);

                if (possibilities.Contains(box[i].Value))
                    possibilities.Remove(box[i].Value);
            }

            if (possibilities.Count == 1)
            {
                puzzle.Cells[r, c].Value = possibilities.Single();
                return true;
            }
            return false;
        }
    }
}
