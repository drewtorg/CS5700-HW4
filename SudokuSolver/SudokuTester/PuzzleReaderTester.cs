using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SudokuSolver.Solver;

namespace SudokuTester
{
    [TestClass]
    public class PuzzleReaderTester
    {

        [TestInitialize]
        public void Init()
        {
            string puzzle4 = "4\n1 2 3 4\n4 2 - 1\n- - -2\n3 - 2 -\n-4 - 3";
            string puzzle9 = "9\n1 2 3 4 5 6 7 8 9\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -\n- - - - - - - - -";
            File.WriteAllText("test4.txt", puzzle4);
            File.WriteAllText("test9.txt", puzzle9);
        }

        [TestMethod]
        public void ReadPuzzle4x4()
        {
            string[] expectedRows = "42-1\n---2\n3-2-\n-4-3".Split();
            Puzzle expected4 = new Puzzle(4, "1234", expectedRows);
            Puzzle actual4 = PuzzleReader.ReadPuzzle("test4.txt");

            CollectionAssert.AreEqual(expected4.Cells, actual4.Cells);
            CollectionAssert.AreEqual(expected4.CharacterSet, actual4.CharacterSet);
            Assert.AreEqual(expected4.TotalRows, actual4.TotalRows);
            
        }

        [TestMethod]
        public void ReadPuzzleNull()
        {
            Puzzle actual = PuzzleReader.ReadPuzzle(null);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ReadPuzzleDoesntExist()
        {
            Puzzle actual = PuzzleReader.ReadPuzzle("asdjaslidoiasd");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ReadPuzzle9x9()
        {
            string[] expectedRows = "---------\n---------\n---------\n---------\n---------\n---------\n---------\n---------\n---------".Split();
            Puzzle expected9 = new Puzzle(9, "123456789", expectedRows);
            Puzzle actual9 = PuzzleReader.ReadPuzzle("test9.txt");

            CollectionAssert.AreEqual(expected9.Cells, actual9.Cells);
            CollectionAssert.AreEqual(expected9.CharacterSet, actual9.CharacterSet);
            Assert.AreEqual(expected9.TotalRows, actual9.TotalRows);

        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("test4.txt");
            File.Delete("test9.txt");
        }
    }
}
