namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle10
{
    private readonly PuzzleInput _testInput = new("Input/test-input-10a1.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-10.txt");
    private readonly Puzzle10 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(4, _puzzle.PartOne(_puzzle.Preprocess(new PuzzleInput("Input/test-input-10a1.txt"))));
        Assert.Equal(4, _puzzle.PartOne(_puzzle.Preprocess(new PuzzleInput("Input/test-input-10a2.txt"))));
        Assert.Equal(8, _puzzle.PartOne(_puzzle.Preprocess(new PuzzleInput("Input/test-input-10b1.txt"))));
        Assert.Equal(8, _puzzle.PartOne(_puzzle.Preprocess(new PuzzleInput("Input/test-input-10b1.txt"))));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(6599, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(0, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(0, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}