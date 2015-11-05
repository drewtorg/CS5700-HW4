using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class Puzzle
    {
        public int TotalRows { get; }
        public List<char> CharacterSet { get; }
        public Cell[,] Cells { get; set; }

        public Cell[] ColumnAt(int col)
        {
            if (col < 0 || col > TotalRows - 1)
                return null;

            Cell[] column = new Cell[TotalRows];

            for (int i = 0; i < TotalRows; i++)
                column[i] = Cells[i, col];

            return column;
        }

        public Cell[] RowAt(int r)
        {
            if (r< 0 || r > TotalRows - 1)
                return null;

            Cell[] row = new Cell[TotalRows];

            for (int i = 0; i < TotalRows; i++)
                row[i] = Cells[r, i];

            return row;
        }

        public Cell[] BoxAt(int r, int c)
        {
            if (c < 0 || c > TotalRows - 1 || r < 0 || r > TotalRows - 1)
                return null;

            int cellSize = (int) Math.Sqrt(TotalRows);
            List<Cell> box = new List<Cell>(TotalRows);

            int rowNum = (r / cellSize) * cellSize;
            int colNum = (c / cellSize) * cellSize;

            for (int i = rowNum; i < rowNum + cellSize; i++)
                for (int j = colNum; j < colNum + cellSize; j++)
                    box.Add(Cells[i, j]);

            return box.ToArray();
        }

        public Puzzle(int numRows, string charSet, string[] rows)
        {
            if (numRows < 0 || charSet == null || charSet == string.Empty || rows == null)
                throw new ArgumentException("Invalid argument");

            TotalRows = numRows;
            CharacterSet = charSet.ToList();
            Cells = ConvertToCells(rows);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(TotalRows.ToString());

            foreach(char mander in CharacterSet)
                builder.Append(mander + " ");

            builder.AppendLine();
            for (int i = 0; i < TotalRows; i++)
            {
                for (int j = 0; j < TotalRows; j++)
                    builder.AppendFormat("{0} ", Cells[i, j].Value);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public bool Solve()
        {
            bool solved = false;
            
            Solver solver = new OneLeftSolver();
            solved = solver.SolvePuzzle(this) ? true : solved;

            solver = new DepthFirstSolver();
            solved = solver.SolvePuzzle(this) ? true : solved;
            
            return solved;
        }

        private Cell[,] ConvertToCells(string[] rows)
        {
            Cell[,] cells = new Cell[rows.Length, rows.Length];
            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < rows[i].Length; j++)
                    cells[i,j] = new Cell(rows[i][j], i, j);
            return cells;
        }
    }
}
