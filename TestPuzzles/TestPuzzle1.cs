namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle1
{
    private readonly PuzzleInput _testInput = new("Input/test-input-01.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-01.txt");
    private readonly Puzzle1 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(142, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(54877, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(281, _puzzle.PartTwo(_puzzle.Preprocess(new PuzzleInput("Input/test-input-01b.txt"))));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(54100, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}