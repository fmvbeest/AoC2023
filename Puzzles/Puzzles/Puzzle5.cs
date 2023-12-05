using System.Collections;

namespace AoC2023.Puzzles;

public class Puzzle5 : PuzzleBase<IEnumerable<IEnumerable<string>>, long, int>
{
    protected override string Filename => "Input/puzzle-input-05.txt";
    protected override string PuzzleTitle => "--- Day 5: If You Give A Seed A Fertilizer ---";

    private IEnumerable<MapRange> ParseMapData(IEnumerable<string> mapData)
    {
        var map = new List<MapRange>();
        mapData = mapData.ToArray();

        foreach (var line in mapData)
        {
            if (line.Trim().EndsWith(":") || string.IsNullOrEmpty(line)) continue;

            var rangeData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse).ToArray();

            map.Add(new MapRange()
            {
                Start = rangeData[1],
                End = rangeData[1] + rangeData[2] - 1,
                Offset = rangeData[0] - rangeData[1]
            });
        }

        return map;
    }
    
    private long ParseInput(IEnumerable<IEnumerable<string>> input, int part = 1)
    {
        var groupedData = input.ToArray();

        var seeds = groupedData[0].First()
            .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
        
        var minLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var categoryMap in groupedData.Skip(1))
            {
                foreach (var mapRange in ParseMapData(categoryMap))
                {
                    if (mapRange.InRange(value))
                    {
                        value += mapRange.Offset;
                        break;
                    }
                }
            }
            minLocation = Math.Min(minLocation, value);
        }

        return minLocation;
    }
    
    public override long PartOne(IEnumerable<IEnumerable<string>> input)
    {
        return ParseInput(input);
    }

    public override int PartTwo(IEnumerable<IEnumerable<string>> input)
    {
        return 0;
    }

    private class MapRange
    {
        public long Start { get; set; }
        public long End { get; set; }
        
        public long Offset { get; set; }

        public bool InRange(long x)
        {
            return x >= Start && x <= End;
        }
    }
    
    public override IEnumerable<IEnumerable<string>> Preprocess(IPuzzleInput input, int part = 1)
    {
        var index = 0;
        return input.GetAllLines().
            GroupBy(x => !string.IsNullOrEmpty(x) ? index : index++);
    }
}