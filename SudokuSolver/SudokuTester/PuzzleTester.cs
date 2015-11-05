using System;
using System.IO;
using System.Linq;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SudokuSolver.Solver;

namespace SudokuTester
{
    [TestClass]
    public class PuzzleTester
    {
        string[] rows = "42-1\n---2\n3-2-\n-4-3".Split();
        Puzzle puzzle;

        [TestInitialize]
        public void Init()
        {
            puzzle = new Puzzle(4, "1234", rows);
        }

        [TestMethod]
        public new void ToString()
        {
            string expected = Assert.ReplaceNullChars("4\r\n1 2 3 4 \r\n4 2 - 1 \r\n- - - 2 \r\n3 - 2 - \r\n- 4 - 3 \r\n");
            string actual = Assert.ReplaceNullChars(puzzle.ToString());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Solve()
        {
            string[] solveMeRows = "4444\n----\n----\n----".Split();
            Puzzle solveMePuzzle = new Puzzle(4, "1234", solveMeRows);
            bool solved = solveMePuzzle.Solve();

            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void CantSolve()
        {
            string[] solvedRows = "4231\n1342\n3124\n2413".Split();
            Puzzle solvedPuzzle = new Puzzle(4, "1234", solvedRows);
            bool solved = puzzle.Solve();

            Assert.IsTrue(solved);
            CollectionAssert.AreEqual(puzzle.Cells, solvedPuzzle.Cells);
        }

        [TestMethod]
        public void RowAt4x4()
        {
            Cell[] expectedRow1 = new Cell[4];
            Cell[] expectedRow2 = new Cell[4];
            Cell[] expectedRow3 = new Cell[4];
            Cell[] expectedRow4 = new Cell[4];
            for (int i = 0; i < 4; i++)
            {
                expectedRow1[i] = new Cell(rows[0].ToCharArray()[i], 0, i);
                expectedRow2[i] = new Cell(rows[1].ToCharArray()[i], 1, i);
                expectedRow3[i] = new Cell(rows[2].ToCharArray()[i], 2, i);
                expectedRow4[i] = new Cell(rows[3].ToCharArray()[i], 3, i);
            }

            var actualRow1 = puzzle.RowAt(0);
            var actualRow2 = puzzle.RowAt(1);
            var actualRow3 = puzzle.RowAt(2);
            var actualRow4 = puzzle.RowAt(3);

            CollectionAssert.AreEqual(expectedRow1, actualRow1);
            CollectionAssert.AreEqual(expectedRow2, actualRow2);
            CollectionAssert.AreEqual(expectedRow3, actualRow3);
            CollectionAssert.AreEqual(expectedRow4, actualRow4);
        }


        [TestMethod]
        public void RowAtNegative()
        {
            var actual = puzzle.RowAt(-1);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void RowAtLarge()
        {
            var actual = puzzle.RowAt(10);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ColumnAt4x4()
        {
            string[] cols = "4-3-\n2--4\n--2-\n12-3".Split();

            Cell[] expectedCol1 = new Cell[4];
            Cell[] expectedCol2 = new Cell[4];
            Cell[] expectedCol3 = new Cell[4];
            Cell[] expectedCol4 = new Cell[4];
            for (int i = 0; i < 4; i++)
            {
                expectedCol1[i] = new Cell(cols[0].ToCharArray()[i], i, 0);
                expectedCol2[i] = new Cell(cols[1].ToCharArray()[i], i, 1);
                expectedCol3[i] = new Cell(cols[2].ToCharArray()[i], i, 2);
                expectedCol4[i] = new Cell(cols[3].ToCharArray()[i], i, 3);
            }           

            var actualCol1 = puzzle.ColumnAt(0);
            var actualCol2 = puzzle.ColumnAt(1);
            var actualCol3 = puzzle.ColumnAt(2);
            var actualCol4 = puzzle.ColumnAt(3);

            CollectionAssert.AreEqual(expectedCol1, actualCol1);
            CollectionAssert.AreEqual(expectedCol2, actualCol2);
            CollectionAssert.AreEqual(expectedCol3, actualCol3);
            CollectionAssert.AreEqual(expectedCol4, actualCol4);
        }

        [TestMethod]
        public void ColumnAtNegative()
        {
            var actual = puzzle.ColumnAt(-1);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ColumnAtLarge()
        {
            var actual = puzzle.ColumnAt(10);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void BoxAt4x4()
        {
            string[] boxes = "42--\n-1-2\n3--4\n2--3".Split();

            var actualBox1 = puzzle.BoxAt(0, 0);
            var actualBox2 = puzzle.BoxAt(0, 2);
            var actualBox3 = puzzle.BoxAt(2, 0);
            var actualBox4 = puzzle.BoxAt(2, 2);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(boxes[0][i], actualBox1[i].Value);
                Assert.AreEqual(boxes[1][i], actualBox2[i].Value);
                Assert.AreEqual(boxes[2][i], actualBox3[i].Value);
                Assert.AreEqual(boxes[3][i], actualBox4[i].Value);
            }
        }

        [TestMethod]
        public void BoxAtNegative()
        {
            var actual1 = puzzle.BoxAt(-1, 0);
            var actual2 = puzzle.BoxAt(0, -1);
            Assert.IsNull(actual1);
            Assert.IsNull(actual2);
        }

        [TestMethod]
        public void BoxAtLarge()
        {
            var actual1 = puzzle.BoxAt(10, 0);
            var actual2 = puzzle.BoxAt(0, 10);
            Assert.IsNull(actual1);
            Assert.IsNull(actual2);
        }
    }
}
