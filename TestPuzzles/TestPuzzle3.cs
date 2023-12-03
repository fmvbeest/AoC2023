namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle3
{
    private readonly PuzzleInput _testInput = new("Input/test-input-03.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-03.txt");
    private readonly Puzzle3 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(4361, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(553825, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(467835, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(93994191, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}