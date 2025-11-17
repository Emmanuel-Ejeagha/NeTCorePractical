using System;

namespace SudokuSolver;

public interface IBoard
{
    int this[int row, int column] { get; set; }
    int Size { get; }
    int GridSize { get; }
}

