namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle4
{
    private readonly PuzzleInput _testInput = new("Input/test-input-04.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-04.txt");
    private readonly Puzzle4 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(13, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(21138, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(30, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(7185540, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}