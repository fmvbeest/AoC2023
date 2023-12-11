using System.Collections;
using System.Xml.Serialization;
using AoC2023.Util;

namespace AoC2023.Puzzles;

public class Puzzle11 : PuzzleBase<IEnumerable<string>, int, long>
{
    protected override string Filename => "Input/puzzle-input-11.txt";
    protected override string PuzzleTitle => "--- Day 11: {{Cosmic Expansion}} ---";

    private static IEnumerable<Coordinate> ParseInput(IEnumerable<string> input, int expandFactor)
    {
        var xIndices = new List<int>();
        var yIndices = new List<int>();

        input = input.ToList();
        
        var yIndex = 0;
        foreach (var line in input)
        {
            if (line.Distinct().Count() == 1)
            {
                yIndices.Add(yIndex + yIndices.Count * (expandFactor - 1));
            }
            yIndex++;
        }

        var transposed = input.Transpose();
        var xIndex = 0;
        foreach (var x in transposed)
        {
            if (x.Distinct().Count() == 1)
            {
                xIndices.Add(xIndex + xIndices.Count * (expandFactor - 1));
            }
            xIndex++;
        }

        return GetGalaxies(input, xIndices, yIndices, expandFactor);
    }

    private static IEnumerable<Coordinate> GetGalaxies(IEnumerable<string> input, 
        ICollection<int> xIndices, ICollection<int> yIndices, int expandFactor)
    {
        var galaxies = new List<Coordinate>();
        
        var row = 0;
        foreach (var line in input)
        {
            if (yIndices.Contains(row))
            {
                row += expandFactor - 1;
            }
            
            var index = 0;
            foreach (var c in line)
            {
                if (xIndices.Contains(index))
                {
                    index += expandFactor - 1;
                }
                
                if (c == '#')
                {
                    galaxies.Add((index, row));
                }
                index++;
            }
            row++;
        }

        return galaxies;
    }
    
    private static long SumManhattanDistance(IReadOnlyList<Coordinate> galaxies)
    {
        return galaxies.Select((c, i) => galaxies.Skip(i + 1).Sum(c.ManhattanDistance)).Sum();
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return (int)SumManhattanDistance(ParseInput(input, expandFactor:2).ToArray());
    }

    public override long PartTwo(IEnumerable<string> input)
    {
        return SumManhattanDistance(ParseInput(input, expandFactor:1000000).ToArray());
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}