using AoC2023.Util;

namespace AoC2023.Puzzles;

public class Puzzle10 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-10.txt";
    protected override string PuzzleTitle => "--- Day 10: {{TITLE}} ---";


    private static Dictionary<Coordinate, Pipe> ParseInput(IEnumerable<string> input, out Coordinate startPosition)
    {
        var pipes = new Dictionary<Coordinate, Pipe>();
        var pipeSymbols = new[] { '|', '-', 'L', 'J', '7', 'F', 'S' };
        startPosition = (-1, -1);
        
        var row = 0;
        foreach (var line in input)
        {
            var index = 0;
            foreach (var c in line)
            {
                if (pipeSymbols.Contains(c))
                {
                    var pipe = ParsePipe(c, index, row);
                    pipes.Add(pipe.Location, pipe);
                    if (c == 'S')
                    {
                        startPosition = pipe.Location;
                    }
                }
                index++;
            }
            row++;
        }
        return pipes;
    }

    private static Pipe ParsePipe(char c, int x, int y)
    {
        var location = new Coordinate(x, y);

        return c switch
        {
            '|' => new Pipe(location, new []{ location - (0,1), location + (0,1) }),
            '-' => new Pipe(location, new []{ location - (1,0), location + (1,0) }),
            'L' => new Pipe(location, new []{ location - (0,1), location + (1,0) }),
            'J' => new Pipe(location, new []{ location - (0,1), location - (1,0) }),
            '7' => new Pipe(location, new []{ location - (1,0), location + (0,1) }),
            'F' => new Pipe(location, new []{ location + (0,1), location + (1,0) }),
            'S' => new Pipe(location, new Coordinate[2]),
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }

    public override int PartOne(IEnumerable<string> input)
    {
        var pipes = ParseInput(input, out var startPosition);
        var startNeighbours = startPosition.Neighbours();
        var connectsTo = startNeighbours.Where(sn => pipes.ContainsKey(sn) && 
                                                     pipes[sn].ConnectsTo.Contains(startPosition)).ToArray();

        var startPipe = new Pipe(startPosition, connectsTo);
        pipes[startPipe.Location] = startPipe;
        
        // Traverse Pipeloop
        var previous = startPipe;
        var current = pipes[startPipe.ConnectsTo.First()];
        var steps = 1;
        
        while (!current.Location.Equals(startPipe.Location))
        {
            if (current.ConnectsTo.Contains(previous.Location))
            {
                var nextLocation = current.ConnectsTo.Single(c => !c.Equals(previous.Location));
                previous = current;
                current = pipes[nextLocation];
                steps++;
            }
            else
            {
                throw new ApplicationException("Current does not connect to previous");
            }
        }
            
        
        return steps/2;
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return 0;
    }

    public class Pipe
    {
        public readonly Coordinate Location;
        public readonly Coordinate[] ConnectsTo;

        public Pipe(Coordinate location, Coordinate[] connectsTo)
        {
            Location = location;
            ConnectsTo = connectsTo;
        }
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}