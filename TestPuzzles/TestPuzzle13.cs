namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle13
{
    private readonly PuzzleInput _testInput = new("Input/test-input-13.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-13.txt");
    private readonly Puzzle13 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(405, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(31265, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(400, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(39359, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}