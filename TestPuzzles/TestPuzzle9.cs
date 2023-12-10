namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle9
{
    private readonly PuzzleInput _testInput = new("Input/test-input-09.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-09.txt");
    private readonly Puzzle9 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(114, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(1637452029, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(2, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(908, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}