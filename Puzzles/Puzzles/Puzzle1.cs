﻿namespace AoC2023.Puzzles;

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
            if (!int.TryParse(c.ToString(), out var digit)) continue;
            if (firstDigit == -1)
            {
                firstDigit = digit;
            }
            lastDigit = digit;
        }
        
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    private static string ReplaceTextualDigits(string s)
    {
        return s.Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "t3e")
            .Replace("four", "f4r")
            .Replace("five", "f5e")
            .Replace("six", "s6x")
            .Replace("seven", "s7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e");
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