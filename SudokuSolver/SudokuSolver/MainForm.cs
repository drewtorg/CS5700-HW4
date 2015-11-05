using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SudokuSolver.Solver;

namespace SudokuSolver
{
    public partial class MainForm : Form
    {
        Puzzle puzzle;


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
            }
        }

        private void RefreshPuzzle()
        {
            tablePanel.Controls.Clear();

            tablePanel.RowCount = puzzle.TotalRows;
            tablePanel.RowStyles.Clear();
            for (int i = 0; i < tablePanel.RowCount; i++)
                tablePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tablePanel.ColumnCount = puzzle.TotalRows;
            tablePanel.ColumnStyles.Clear();
            for (int i = 0; i < tablePanel.ColumnCount; i++)
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            foreach (var cell in puzzle.Cells)
            {
                Button b = new Button();
                //b.Enabled = false;
                b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                b.AutoSize = true;
                b.BackColor = BackColor;
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

        private void solveButton_Click(object sender, EventArgs e)
        {
            puzzle.Solve();

            RefreshPuzzle();
        }
    }
}
