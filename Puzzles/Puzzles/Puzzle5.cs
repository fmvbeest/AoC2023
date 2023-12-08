using AoC2023.Util.Fertilizer;

namespace AoC2023.Puzzles;

public class Puzzle5 : PuzzleBase<IEnumerable<IEnumerable<string>>, long, long>
{
    protected override string Filename => "Input/puzzle-input-05.txt";
    protected override string PuzzleTitle => "--- Day 5: If You Give A Seed A Fertilizer ---";

    private static IEnumerable<long> ParseSeeds(string seedData)
    {
        return seedData.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
    }
    
    private static IEnumerable<MapRange> ParseMapData(IEnumerable<string> mapData, int part = 1)
    {
        mapData = mapData.ToArray();
        var map = new List<MapRange>();
        var source = part % 2;
        var target = part - 1;
        
        foreach (var line in mapData)
        {
            if (line.Trim().EndsWith(":") || string.IsNullOrEmpty(line)) continue;

            var rangeData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse).ToArray();

            map.Add(new MapRange()
            {
                Start = rangeData[source],
                End = rangeData[source] + rangeData[2] - 1,
                Offset = rangeData[target] - rangeData[source]
            });
        }

        return map;
    }

    private static bool IsValidSeed(long value, IEnumerable<(long, long)> seedRanges)
    {
        foreach (var (start, end) in seedRanges)
        {
            if (value >= start && value <= end)
            {
                return true;
            }
        }
        return false;
    }
    
    public override long PartOne(IEnumerable<IEnumerable<string>> input)
    {
        var groupedData = input.ToArray();
        var categoryMaps = groupedData.Skip(1).Select(mapData => ParseMapData(mapData)).ToArray();
        var seeds = ParseSeeds(groupedData[0].First());
        
        var minLocation = long.MaxValue;
        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var categoryMap in categoryMaps)
            {
                foreach (var mapRange in categoryMap)
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

    public override long PartTwo(IEnumerable<IEnumerable<string>> input)
    {
        var groupedData = input.ToArray();
        var categoryMaps = groupedData.Skip(1).Select(mapData => ParseMapData(mapData, part:2)).Reverse().ToArray();
        var seeds = ParseSeeds(groupedData[0].First()).ToArray();
        var seedRanges = seeds.Chunk(2).Select(x => (x[0], x[1] + x[0] - 1)).ToArray();
        
        var minLocation = -1L;
        var terminate = false;
        while (!terminate)
        {
            var value = ++minLocation;
            foreach (var categoryMap in categoryMaps)
            {
                foreach (var mapRange in categoryMap)
                {
                    if (mapRange.InRange(value))
                    {
                        value += mapRange.Offset;
                        break;
                    }
                }
            }

            terminate = IsValidSeed(value, seedRanges);
        }

        return minLocation;
    }
    
    public override IEnumerable<IEnumerable<string>> Preprocess(IPuzzleInput input, int part = 1)
    {
        var index = 0;
        return input.GetAllLines().
            GroupBy(x => !string.IsNullOrEmpty(x) ? index : index++);
    }
}