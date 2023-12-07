namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle7
{
    private readonly PuzzleInput _testInput = new("Input/test-input-07.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-07.txt");
    private readonly Puzzle7 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(6440, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(250453939, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(5905, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(248652697, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}