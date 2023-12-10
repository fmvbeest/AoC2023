namespace AoC2023.Puzzles;

public class Puzzle9 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-09.txt";
    protected override string PuzzleTitle => "--- Day 9: Mirage Maintenance ---";

    private static int ParseRecursive(int[] sequence)
    {
        if (sequence.Distinct().Count() == 1)
        {
            return sequence.Last();
        }
        
        var diffSequence = sequence.Zip(sequence.Skip(1), (a, b) => b - a).ToArray();
        return sequence.Last() + ParseRecursive(diffSequence);
    }

    private static int[] ParseHistory(string input)
    {
        return input.Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(line => ParseRecursive(ParseHistory(line)));
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return input.Select(ParseHistory).Select(history => 
            ParseRecursive(history.Reverse().ToArray())).Sum();
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}