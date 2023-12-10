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
        var diffSequence = new List<int>();
        for (var i = 1; i < sequence.Length; i++)
        {
            diffSequence.Add(sequence[i] - sequence[i-1]);
        }
        
        return sequence.Last() + ParseRecursive(diffSequence.ToArray());
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
            ParseRecursive(history.Reverse().ToArray())).ToList().Sum();
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}