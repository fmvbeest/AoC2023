using AoC2023.Util.MathHelper;

namespace AoC2023.Puzzles;

public class Puzzle8 : PuzzleBase<IEnumerable<string>, int, long>
{
    protected override string Filename => "Input/puzzle-input-08.txt";
    protected override string PuzzleTitle => "--- Day 8: Haunted Wasteland ---";

    private Dictionary<string, (string Left, string Right)> ParseMaps(IEnumerable<string> input)
    {
        var maps = new Dictionary<string, (string, string)>();

        foreach (var line in input)
        {
            var parts = line.Split('=', StringSplitOptions.TrimEntries);
            var key = parts[0];
            parts = parts[1].Split(',', StringSplitOptions.TrimEntries);
            var left = parts[0][1..];
            var right = parts[1][..^1];
            maps.Add(key, (left, right));
        }

        return maps;
    }
    
    private static int TraverseToEnd(string start, string end, string choicePattern, 
        IReadOnlyDictionary<string, (string left, string right)> maps)
    {
        var current = start;
        var finished = false;
        var steps = 0;

        while (!finished)
        {
            foreach (var t in choicePattern)
            {
                var choices = maps[current];
                current = t switch
                {
                    'L' => choices.left,
                    'R' => choices.right,
                    _ => current
                };
                steps++;
                if (current.EndsWith(end))
                {
                    finished = true;
                    break;
                }
            }
        }

        return steps;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        input = input.ToArray();
        
        var choicePattern = input.ToArray().First();

        var maps = ParseMaps(input.Skip(2));

        return TraverseToEnd("AAA", "ZZZ", choicePattern, maps);
    }
    
    public override long PartTwo(IEnumerable<string> input)
    {
        input = input.ToArray();
        var choicePattern = input.ToArray().First();
        var maps = ParseMaps(input.Skip(2));
        var startPositions = maps.Keys.Where(key => key.EndsWith("A")).ToList();

        var stepsList = startPositions.Select(start => TraverseToEnd(start, "Z", choicePattern, maps))
            .Select(steps => (long)steps).ToList();

        return stepsList.Aggregate(1L, Functions.LeastCommonMultiple);
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}