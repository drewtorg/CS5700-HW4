﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class DepthFirstSolver : Solver
    {
        public override bool SolveCell(Puzzle puzzle, int r, int c)
        {
            if (r == puzzle.TotalRows)
                return true;

            if (puzzle.Cells[r, c].Value != '-')
            {
                if (c == puzzle.TotalRows - 1)
                {
                    if (SolveCell(puzzle, r + 1, 0))
                        return true;
                }
                else if (SolveCell(puzzle, r, c + 1))
                    return true;
                return false;
            }

            foreach (var nextValue in puzzle.CharacterSet)
            {
                if (IsValidMove(puzzle, r, c, nextValue))
                {
                    puzzle.Cells[r, c].Value = nextValue;
                    if (c == puzzle.TotalRows - 1)
                    {
                        if (SolveCell(puzzle, r + 1, 0))
                            return true;
                    }
                    else if (SolveCell(puzzle, r, c + 1))
                        return true;
                }
            }

            puzzle.Cells[r, c].Value = '-';
            return false;
        }

        private bool IsValidMove(Puzzle puzzle, int r, int c, char value)
        {
            var row = puzzle.RowAt(r);
            var col = puzzle.ColumnAt(c);

            for (int i = 0; i < puzzle.TotalRows; i++)
            {
                if (row[i].Value == value)
                    return false;

                if (col[i].Value == value)
                    return false;
            }

            return true;
        }
    }
}
