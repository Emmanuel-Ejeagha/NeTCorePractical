using System;

namespace SudokuSolver;

public class ArrayBoard : IBoard
{
    private readonly int[,] _boardArrays;
    private readonly int _size;
    private readonly int _gridSize;

    public int this[int row, int column]
    {
        get => _boardArrays[row, column];
        set => _boardArrays[row, column] = value;
    }

    public int Size => _size;
    public int GridSize => _gridSize;

    public ArrayBoard(int size)
    {
        if (!IsValidSize(size, out _gridSize))
        {
            throw new ArgumentException($"Invalid size: {size}", nameof(size));
        }
        _boardArrays = new int[size, size];
        _size = size;
    }

    private static bool IsValidSize(int size, out int sqrt)
    {
        sqrt = (int)Math.Sqrt(size);
        return size >= 4 && (sqrt * sqrt) == size;
    }
}
