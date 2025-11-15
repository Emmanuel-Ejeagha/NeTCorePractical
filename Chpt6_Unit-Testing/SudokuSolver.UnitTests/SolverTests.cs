using SudokuSolver;

namespace SudokuSolver.UnitTests;

public class SolverTests
{
    [Fact]
    public void Empty4x4Board()
    {
        int[,] empty = new int[4, 4];
        var solver = new Solver(empty);
        Assert.True(solver.Isvalid());
    }

    [Fact]
    public void NonSquareBoard()
    {
        int[,] empty = new int[4, 9];
        var solver = new Solver(empty);
        Assert.False(solver.Isvalid());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(4, true)]
    [InlineData(8, false)]
    [InlineData(9, true)]
    [InlineData(10, false)]
    [InlineData(16, true)]
    public void EmptyBoardSizes(int size, bool isvalid)
    {
        int[,] empty = new int[size, size];
        var solver = new Solver(empty);
        Assert.Equal(isvalid, solver.Isvalid());
    }
}
