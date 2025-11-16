namespace SudokuSolver;

public class Solver
{
    private readonly int[,] _board;

    public Solver(int[,] board)
    {
        _board = board;
    }

    public bool Isvalid()
    {
        int rows = _board.GetLength(0);
        int cols = _board.GetLength(1);

        if (rows != cols)
            return false;

        if (rows < 4)
            return false;

        int sqrt = (int)Math.Sqrt(rows);
        if ((sqrt * sqrt) != rows)
            return false;

        return true;
    }

    public interface IBoard
    {
        int this[int row, int column] { get; set; }
        int Size { get; }
        int GridSize { get; }
    }
}
