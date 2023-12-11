namespace TestPuzzles;
using AoC2023.Puzzles;

public class TestPuzzle11
{
    private readonly PuzzleInput _testInput = new("Input/test-input-11.txt");
    private readonly PuzzleInput _puzzleInput = new("Input/puzzle-input-11.txt");
    private readonly Puzzle11 _puzzle = new();

    [Fact]
    public void TestPartOneSample()
    {
        Assert.Equal(374, _puzzle.PartOne(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartOneActual()
    {
        Assert.Equal(9639160, _puzzle.PartOne(_puzzle.Preprocess(_puzzleInput)));
    }
    
    [Fact]
    public void TestPartTwoSample()
    {
        Assert.Equal(82000210, _puzzle.PartTwo(_puzzle.Preprocess(_testInput)));
    }
    
    [Fact]
    public void TestPartTwoActual()
    {
        Assert.Equal(752936133304, _puzzle.PartTwo(_puzzle.Preprocess(_puzzleInput)));
    }
}