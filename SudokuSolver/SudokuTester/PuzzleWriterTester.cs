using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SudokuSolver.Solver;

namespace SudokuTester
{
    [TestClass]
    public class PuzzleWriterTester
    {
        Puzzle puzzle;
        [TestInitialize]
        public void Init()
        {
            puzzle = new Puzzle(4, "1234", "42-1\n---2\n3-2-\n-4-3".Split());
        }

        [TestMethod]
        public void WritePuzzle4x4()
        {
            string expected = "4\r\n1 2 3 4 \r\n4 2 - 1 \r\n- - - 2 \r\n3 - 2 - \r\n- 4 - 3 \r\n";
            PuzzleWriter.WritePuzzle("test.txt", puzzle);
            string actual = File.ReadAllText("test.txt");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WritePuzzleNullFile()
        {
            PuzzleWriter.WritePuzzle(null, puzzle);
        }

        [TestMethod]
        public void WritePuzzleNullPuzzle()
        {
            PuzzleWriter.WritePuzzle("test.txt", null);
            Assert.IsFalse(File.Exists("test.txt"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            if(File.Exists("test.txt"))
                File.Delete("test.txt");
        }
    }
}
