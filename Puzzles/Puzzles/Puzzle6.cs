namespace AoC2023.Puzzles;

public class Puzzle6 : PuzzleBase<IEnumerable<string>, int, int>
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

    private static int Wins(Race race)
    {
        var a = -1;
        var b = race.Duration;
        var c = race.Record * - 1;

        var d = b * b - 4 * a * c;

        var x1 = Convert.ToInt32(Math.Floor((-b + Math.Sqrt(d)) / (2 * a)));
        var x2 = Convert.ToInt32(Math.Ceiling((-b - Math.Sqrt(d)) / (2 * a)));
        
        return x2 - x1 - 1;
    }
    
    
    public override int PartOne(IEnumerable<string> input)
    {
        return ParseRaces(input).Aggregate(1, (current, race) => current * Wins(race));
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return Wins(ParseSingleRace(input));
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