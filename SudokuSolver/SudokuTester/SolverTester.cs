using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SudokuSolver.Solver;

namespace SudokuTester
{
    [TestClass]
    public class SolverTester
    {
        string[] rows = "42-1\n-3-2\n3-2-\n-4-3".Split();
        Puzzle puzzle;
        Solver solver;

        [TestInitialize]
        public void Init()
        {
            puzzle = new Puzzle(4, "1234", rows);
        }

        [TestMethod]
        public void LastOneRow()
        {
            solver = new OneLeftSolver();
            bool solvedIt = solver.SolveCell(puzzle, 0, 2);

            Assert.IsTrue(solvedIt);
            Assert.AreEqual(puzzle.Cells[0, 2].Value, '3');
        }

        [TestMethod]
        public void LastOneColumn()
        {
            solver = new OneLeftSolver();
            bool solvedIt = solver.SolveCell(puzzle, 2, 3);

            Assert.IsTrue(solvedIt);
            Assert.AreEqual(puzzle.Cells[2, 3].Value, '4');
        }

        [TestMethod]
        public void LastOneBox()
        {
            solver = new OneLeftSolver();
            bool solvedIt = solver.SolveCell(puzzle, 1, 0);

            Assert.IsTrue(solvedIt);
            Assert.AreEqual(puzzle.Cells[1, 0].Value, '1');
        }

        [TestMethod]
        public void LastOneCantSolve()
        {
            solver = new OneLeftSolver();
            bool solvedIt = solver.SolveCell(puzzle, 3, 0);

            Assert.IsFalse(solvedIt);
        }

        [TestMethod]
        public void DepthFirstSolve()
        {
            solver = new DepthFirstSolver();
            bool solvedIt = solver.SolveCell(puzzle, 0, 0);

            Assert.IsTrue(solvedIt);
            Assert.IsTrue(solver.IsSolved(puzzle));
        }

        [TestMethod]
        public void DepthFirstCantSolve()
        {
            solver = new DepthFirstSolver();
            puzzle.Cells[0, 2].Value = '4';
            bool solvedIt = solver.SolveCell(puzzle, 0, 0);

            Assert.IsFalse(solvedIt);
        }
    }
}
