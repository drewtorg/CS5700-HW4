C# Sudoku Solver

To use the sudoku solver GUI, simply  run the code or run the .exe file in the debug folder.

In the toolbar at the top, click "File" then "Load" and pick the file containing the puzzle data.

Then you can click the solve button and it will solve the sudoku puzzle.

You can save the output by clicking on "File" and then "Save" and then choosing the save location.

The solver works for the 4x4, 9x9, 16x16, and 25x25 puzzle sizes.  Because of the
nature of the solution, the 16x16 and 25x25 sized puzzles will take a very long time
to finish.  Be patient and it will eventually get an answer.

Of the patterns mentioned in the assignment description, I chose to implement the Template Method
pattern along with a simple strategy pattern, which can be seen in the implementation of the solver class.
I contemplated using a facade for the PuzzleWriter and PuzzleReader classes, but in the end, the
PuzzleReader's code doesn't look at all like the PuzzleWriter and so in order to keep
each class cohesive, I strayed from using a facade.  I didn't find anywhere to
needfully apply the adaptor pattern.

As for the extra credit, I obviously chose to provide a GUI, but without animations,
and the solver will successfully solve puzzles of any difficulty.
