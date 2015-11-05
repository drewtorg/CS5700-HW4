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
            var charSet = new List<char>(puzzle.CharacterSet);

            var row = puzzle.RowAt(r);
            var col = puzzle.ColumnAt(c);
            var box = puzzle.BoxAt(r, c);

            for (int i = 0; i < puzzle.TotalRows; i++)
            {
                if (charSet.Contains(row[i].Value) || 
                    charSet.Contains(col[i].Value) || 
                    charSet.Contains(box[i].Value))
                        charSet.Remove(row[i].Value);
                
            }

            if (charSet.Count == 1)
            {
                puzzle.Cells[r, c].Value = charSet.Single();
                return true;
            }
            return false;
        }
    }
}
