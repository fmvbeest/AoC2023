namespace AoC2023.Puzzles;

public class Puzzle1 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-01.txt";
    protected override string PuzzleTitle => "--- Day 1: Trebuchet?! ---";

    private static int SumLine(string s)
    {
        var firstNum = -1;
        var lastNum = -1;

        foreach (var c in s)
        {
            if (!int.TryParse(c.ToString(), out var number)) continue;
            if (firstNum == -1)
            {
                firstNum = number;
            }
            lastNum = number;
        }
        return int.Parse($"{firstNum}{lastNum}");
    }

    private static string ReplaceTextualNumbers(string line)
    {
        line = line.Replace("one", "o1e");
        line = line.Replace("two", "t2o");
        line = line.Replace("three", "t3e");
        line = line.Replace("four", "f4r");
        line = line.Replace("five", "f5e");
        line = line.Replace("six", "s6x");
        line = line.Replace("seven", "s7n");
        line = line.Replace("eight", "e8t");
        line = line.Replace("nine", "n9e");
        
        return line;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(SumLine);
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return input.Sum(line => SumLine(ReplaceTextualNumbers(line)));
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}