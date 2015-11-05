using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SudokuSolver.Solver;

namespace SudokuSolver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            Puzzle p = PuzzleReader.ReadPuzzle("easy9x9.txt");
            p.Solve();
            Console.WriteLine(p.ToString());
        }
    }
}
