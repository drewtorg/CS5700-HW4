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

        private const int BoxSize = 50;

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
            RefreshTablePanel();

            Size = new Size(tablePanel.Size.Width + 150, tablePanel.Size.Height + 150);
            solveButton.Location = new Point(Size.Width / 2 - solveButton.Size.Width / 2, solveButton.Location.Y);
        }

        private void RefreshTablePanel()
        {
            tablePanel.Controls.Clear();
            tablePanel.Size = new Size(puzzle.TotalRows * BoxSize, puzzle.TotalRows * BoxSize);

            tablePanel.RowCount = puzzle.TotalRows;
            tablePanel.RowStyles.Clear();
            for (int i = 0; i < tablePanel.RowCount; i++)
                tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, BoxSize));

            tablePanel.ColumnCount = puzzle.TotalRows;
            tablePanel.ColumnStyles.Clear();
            for (int i = 0; i < tablePanel.ColumnCount; i++)
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, BoxSize));

            foreach (var cell in puzzle.Cells)
            {
                Point boxNum = puzzle.BoxLocationAt(cell.Row, cell.Column);

                Button b = new Button();
                b.Enabled = false;
                b.BackColor = Color.FromArgb(255 - ((boxNum.X) * 13 % 100), 255 - ((boxNum.Y) * 15 % 100), 200);
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
