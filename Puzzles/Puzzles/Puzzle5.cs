namespace AoC2023.Puzzles;

public class Puzzle5 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-05.txt";
    protected override string PuzzleTitle => "--- Day 5: If You Give A Seed A Fertilizer ---";

    public override int PartOne(IEnumerable<string> input)
    {
        return 0;
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return 0;
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}