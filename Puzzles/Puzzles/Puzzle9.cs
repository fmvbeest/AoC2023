namespace AoC2023.Puzzles;

public class Puzzle9 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-09.txt";
    protected override string PuzzleTitle => "--- Day 9: {{TITLE}} ---";

    private int ParseHistory(string s)
    {
        var history = s.Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        var enumerable = history.ToArray();

        var diffSequences = new List<int[]>();
        diffSequences.Add(history.ToArray());
        
        while (enumerable.Distinct().Count() > 1)
        {
            var array = enumerable.ToArray();
            var tmp = new List<int>();
            for (var i = 1; i < array.Length; i++)
            {
                tmp.Add(array[i] - array[i-1]);
            }

            enumerable = tmp.ToArray();
            diffSequences.Add(tmp.ToArray());
        }

        var store = 0;
         
        diffSequences.Reverse();
        foreach (var sequence in diffSequences)
        {
            store += sequence.Last();
        }

        return store;
    }
    
    private int ParseHistory2(string s)
    {
        var history = s.Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        var enumerable = history.ToArray();

        var diffSequences = new List<int[]>();
        diffSequences.Add(history.ToArray());
        
        while (enumerable.Distinct().Count() > 1)
        {
            var array = enumerable.ToArray();
            var tmp = new List<int>();
            for (var i = 1; i < array.Length; i++)
            {
                tmp.Add(array[i] - array[i-1]);
            }

            enumerable = tmp.ToArray();
            diffSequences.Add(tmp.ToArray());
        }

        var store = 0;

        diffSequences.Reverse();
        
        foreach (var sequence in diffSequences)
        {
            store = (sequence.First() - store);
        }

        return store;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(ParseHistory);
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return input.Sum(ParseHistory2);
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}