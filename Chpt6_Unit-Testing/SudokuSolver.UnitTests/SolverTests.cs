using SudokuSolver;

namespace SudokuSolver.UnitTests;

public class SolverTests
{
    [Fact]
    public void Empty4x4Board()
    {
    //     int[,] empty = new int[4, 4];
    //     var solver = new Solver(empty);
    //     Assert.True(solver.Isvalid());
    // }

    // [Fact]
    // public void NonSquareBoard()
    // {
    //     int[,] empty = new int[4, 9];
    //     var solver = new Solver(empty);
    //     Assert.False(solver.Isvalid());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(4, true)]
    [InlineData(8, false)]
    [InlineData(9, true)]
    [InlineData(10, false)]
    [InlineData(16, true)]
    public void EmptyBoardSizes(int size, bool isValid)
    {
        // int[,] empty = new int[size, size];
        // var solver = new Solver(empty);
        // Assert.Equal(isValid, solver.Isvalid());
        if (!isValid)
        {
            Assert.Throws<ArgumentException>("size", () => new ArrayBoard(size));
        }
        else
        {
            _ = new ArrayBoard(size);
        }
    }

    [Theory]
    [MemberData(nameof(Boards))]
    public void CheckRules(IBoard board, bool isValid)
    {
        var solver = new Solver(board);
        Assert.Equal(isValid, solver.Isvalid());
    }

    public static IEnumerable<object[]> Boards
    {
        get
        {
            // --- 1. Row Repeat Check ---
            IBoard board = new ArrayBoard(4); 
            board[1, 0] = 1; // Row 1, Col 0
            board[1, 3] = 1; // Row 1, Col 3 (Repeat in Row 1)
            yield return new object[] { board, false }; 

            // --- 2. Column Repeat Check ---
            board = new ArrayBoard(4); 
            board[1, 0] = 1; // Row 1, Col 0
            board[3, 0] = 1; // Row 3, Col 0 (Repeat in Col 0)
            yield return new object[] { board, false }; 
            
            // --- 3. Subgrid Repeat Check (Top-Right Grid: Rows 0-1, Cols 2-3) ---
            board = new ArrayBoard(4);
            board[1, 2] = 1; // Row 1, Col 2 (In Subgrid 0, 1)
            board[0, 3] = 1; // Row 0, Col 3 (Repeat in Subgrid 0, 1)
            yield return new object[] { board, false };
            
            // --- 4. Subgrid Repeat Check (Bottom-Right Grid: Rows 2-3, Cols 2-3) ---
            board = new ArrayBoard(4);
            board[3, 3] = 1; // Row 3, Col 3
            board[2, 2] = 1; // Row 2, Col 2 (Repeat in Subgrid 1, 1)
            yield return new object[] { board, false };
            
            // --- 5. Valid (Empty) Board ---
            board = new ArrayBoard(4);
            yield return new object[] { board, true };
            
            // --- 6. Valid Board (No Repeats) ---
            board = new ArrayBoard(4);
            board[0, 0] = 1; 
            board[1, 2] = 2; 
            board[2, 1] = 3; 
            board[3, 3] = 4;
            yield return new object[] { board, true };
        }
    }
}
