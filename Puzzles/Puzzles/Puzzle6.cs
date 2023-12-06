namespace AoC2023.Puzzles;

public class Puzzle6 : PuzzleBase<IEnumerable<string>, int, long>
{
    protected override string Filename => "Input/puzzle-input-06.txt";
    protected override string PuzzleTitle => "--- Day 6: Wait For It ---";

    private static IEnumerable<Race> ParseRaces(IEnumerable<string> input)
    {
        var raceData = input.ToArray();
        var times = raceData[0].Split(':', StringSplitOptions.RemoveEmptyEntries)
            .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
        var distances = raceData[1].Split(':', StringSplitOptions.RemoveEmptyEntries)
            .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();

        return times.Zip(distances, (time, distance) => new Race() { Duration = time, Record = distance });
    }
    
    private static Race ParseSingleRace(IEnumerable<string> input)
    {
        var raceData = input.ToArray();
        var time = long.Parse(raceData[0].Split(':', StringSplitOptions.RemoveEmptyEntries)
            .Last().Replace(" ", string.Empty));
            
        var distance = long.Parse(raceData[1].Split(':', StringSplitOptions.RemoveEmptyEntries)
            .Last().Replace(" ", string.Empty));

        return new Race { Duration = time, Record = distance };
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        var result = 1;

        foreach (var race in ParseRaces(input))
        {
            var wins = 0;
            for (var i = 0; i <= race.Duration; i++)
            {
                if ((race.Duration - i) * i > race.Record)
                {
                    wins++;
                }
            }
            result *= wins;
        }
        
        return result;
    }

    public override long PartTwo(IEnumerable<string> input)
    {
        var race = ParseSingleRace(input);
        
        var wins = 0;
        for (var i = 0; i <= race.Duration; i++)
        {
            if ((race.Duration - i) * i > race.Record)
            {
                wins++;
            }
        }

        return wins;
    }

    private class Race
    {
        public long Duration { get; init; }
        public long Record { get; init; }
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}