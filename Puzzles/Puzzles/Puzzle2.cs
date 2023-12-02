namespace AoC2023.Puzzles;

public class Puzzle2 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-02.txt";
    protected override string PuzzleTitle => "--- Day 2: Cube Conundrum ---";

    private static Game ParseGame(string gameData)
    {
        var gameSummary = gameData.Split(':');
        var id = int.Parse(gameSummary[0].Split(' ')[1].Trim());
        var samples = gameSummary[1].Split(';');

        var sampleList = new List<Sample>();
        var game = new Game(sampleList) {Id = id};
        
        foreach (var sampleData in samples)
        {
            var sample = new Sample();
            foreach (var colourCount in sampleData.Trim().Split(','))
            {
                var count = int.Parse(colourCount.Trim().Split(' ')[0].Trim());
                var color = colourCount.Trim().Split(' ')[1].Trim();
                switch (color)
                {
                    case "red":
                        sample.Red = count;
                        break;
                    case "green":
                        sample.Green = count;
                        break;
                    case "blue":
                        sample.Blue = count;
                        break;
                }
            }
            sampleList.Add(sample);
        }

        return game;
    }
    
    private static int PossibleGameId(string gameData, int limRed, int limGreen, int limBlue)
    {
        var game = ParseGame(gameData);

        return game.Samples().Any(sample => sample.Red > limRed || sample.Green > limGreen || sample.Blue > limBlue) 
            ? 0 : game.Id;
    }
    
    private static int PossibleGamePower(string gameData)
    {
        var game = ParseGame(gameData); 

        var minRed = 0;
        var minGreen = 0;
        var minBlue = 0;
        
        foreach (var sample in game.Samples())
        {
            minRed = Math.Max(minRed, sample.Red);
            minGreen = Math.Max(minGreen, sample.Green);
            minBlue = Math.Max(minBlue, sample.Blue);
        }

        return minRed * minGreen * minBlue;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(line => PossibleGameId(line, 12, 13, 14));
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return input.Sum(PossibleGamePower);
    }

    private class Game
    {
        public int Id { get; set; }
        private readonly IEnumerable<Sample> _samples;

        public Game(IEnumerable<Sample> samples)
        {
            _samples = samples;
        }

        public IEnumerable<Sample> Samples()
        {
            return _samples;
        }
    }
    
    private class Sample
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
    
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}