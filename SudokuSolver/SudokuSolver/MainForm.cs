using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using SudokuSolver.Solver;

namespace SudokuSolver
{
    public partial class MainForm : Form
    {
        Puzzle puzzle;
        public delegate void PuzzleSolver();

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                puzzle = PuzzleReader.ReadPuzzle(openFileDialog.FileName);
                if (puzzle != null)
                {
                    RefreshPuzzle();
                    solveButton.Enabled = true;
                }
                else
                    MessageBox.Show("File is corrupted, check format.");
            }
        }

        private void RefreshPuzzle()
        {
            tablePanel.Controls.Clear();
            tablePanel.Size = new Size(puzzle.TotalRows * 50, puzzle.TotalRows * 50);

            tablePanel.RowCount = puzzle.TotalRows;
            tablePanel.RowStyles.Clear();
            for (int i = 0; i < tablePanel.RowCount; i++)
                tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            tablePanel.ColumnCount = puzzle.TotalRows;
            tablePanel.ColumnStyles.Clear();
            for (int i = 0; i < tablePanel.ColumnCount; i++)
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));

            foreach (var cell in puzzle.Cells)
            {
                int cellSize = (int)Math.Sqrt(puzzle.TotalRows);

                int rowNum = (cell.Row / cellSize) * cellSize;
                int colNum = (cell.Column / cellSize) * cellSize;

                Button b = new Button();
                b.Enabled = false;
                b.BackColor = Color.FromArgb(255 - ((rowNum) * 13 % 100), 255 - ((colNum) * 15 % 100), 200);
                b.ForeColor = Color.Black;
                b.Text = cell.Value.ToString();
                b.Dock = DockStyle.Fill;
                tablePanel.Controls.Add(b);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PuzzleWriter.WritePuzzle(saveFileDialog.FileName, puzzle);
            }
        }

        private async void solveButton_Click(object sender, EventArgs e)
        {
            solveButton.Text = "Solving...";
            solveButton.Enabled = false;
            bool solvedIt = await StartSolve();
            if (solvedIt)
                RefreshPuzzle();
            else
                MessageBox.Show("Puzzle cannot be solved!");
            solveButton.Enabled = true;
            solveButton.Text = "Solve";
        }

        private async Task<bool> StartSolve()
        {
            return await Task.Run(() =>puzzle.Solve());
        }
    }
}
