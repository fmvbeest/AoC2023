namespace AoC2023.Puzzles;

public class Puzzle1 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-01.txt";
    protected override string PuzzleTitle => "--- Day 1: Trebuchet?! ---";

    private static int SumString(string s)
    {
        var firstDigit = -1;
        var lastDigit = -1;

        foreach (var c in s)
        {
            if (!int.TryParse(c.ToString(), out var number)) continue;
            if (firstDigit == -1)
            {
                firstDigit = number;
            }
            lastDigit = number;
        }
        
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    private static string ReplaceTextualDigits(string s)
    {
        s = s.Replace("one", "o1e");
        s = s.Replace("two", "t2o");
        s = s.Replace("three", "t3e");
        s = s.Replace("four", "f4r");
        s = s.Replace("five", "f5e");
        s = s.Replace("six", "s6x");
        s = s.Replace("seven", "s7n");
        s = s.Replace("eight", "e8t");
        s = s.Replace("nine", "n9e");
        
        return s;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(SumString);
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return input.Sum(line => SumString(ReplaceTextualDigits(line)));
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}