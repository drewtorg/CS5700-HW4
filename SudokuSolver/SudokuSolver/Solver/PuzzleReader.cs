using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuSolver.Solver
{
    public static class PuzzleReader
    {
        public static Puzzle ReadPuzzle(string filename)
        {
            if (filename == null)
                return null;
            try {
                using (var reader = File.OpenText(filename))
                {
                    int numRows = int.Parse(reader.ReadLine());
                    string charSet = reader.ReadLine().Replace(" ", "");

                    string[] rows = new string[numRows];
                    for (int i = 0; i < numRows; i++)
                    {
                        string input = reader.ReadLine().Replace(" ", "");
                        if (input != string.Empty)
                            rows[i] = input;
                        else
                            return null;
                    }

                    return new Puzzle(numRows, charSet, rows);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
