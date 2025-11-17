namespace SudokuSolver;

public class Solver
{
    private readonly IBoard _board;

    public Solver(IBoard board)
    {
        _board = board;
    }

    public bool Isvalid()
    {
        // int rows = _board.GetLength(0);
        // int cols = _board.GetLength(1);

        // if (rows != cols)
        //     return false;

        // if (rows < 4)
        //     return false;

        // int sqrt = (int)Math.Sqrt(rows);
        // if ((sqrt * sqrt) != rows)
        //     return false;
        int size = _board.Size;
        var usedSet = new HashSet<int>();
        for (int row = 0; row < size; row++)
        {
            usedSet.Clear();
            for (int col = 0; col < size; col++)
            {
                int num = _board[row, col];
                if (num == 0)
                    continue;
                if (usedSet.Contains(num))
                    return false;
                usedSet.Add(num);
            }
        }

        for (int col = 0; col < size; col++)
        {
            usedSet.Clear();
            for (int row = 0; row < size; row++)
            {
                int num = _board[row, col];
                if (num == 0)
                    continue;
                if (usedSet.Contains(num))
                    return false;
                usedSet.Add(num);
            }
        }

        int sqrt = _board.GridSize;
        for (int grid = 0; grid < size; grid++)
        {
            usedSet.Clear();
            int startCol = (grid % sqrt) * sqrt;
            int startRow = (grid / sqrt) * sqrt;
            for (int cell = 0; cell < size; cell++)
            {
                int col = startCol + (cell % sqrt);
                int row = startRow + (cell / sqrt);
                int num = _board[row, col];
                if (num == 0)
                    continue;
                if (usedSet.Contains(num))
                    return false;
                usedSet.Add(num);
            }
        }
        return true;
    }

}