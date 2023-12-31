namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle5
{
    private readonly PuzzleInput _testInput = new("Input/test-input-05.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-05.txt");
    private readonly Puzzle5 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(35, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(650599855, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(46, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(1240035, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}