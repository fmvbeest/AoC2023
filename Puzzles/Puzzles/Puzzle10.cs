using AoC2023.Util;
using Microsoft.VisualBasic;

namespace AoC2023.Puzzles;

public class Puzzle10 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-10.txt";
    protected override string PuzzleTitle => "--- Day 10: Pipe Maze ---";

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

    private static void UpdateWithStartPosition(IDictionary<Coordinate, Pipe> pipes, Coordinate startPosition)
    {
        var startNeighbours = startPosition.Neighbours();
        var connectsTo = startNeighbours.Where(sn => pipes.ContainsKey(sn) && 
                                                     pipes[sn].ConnectsTo.Contains(startPosition)).ToArray();

        var startPipe = new Pipe(startPosition, connectsTo);
        pipes[startPipe.Location] = startPipe;
    }


    private static IEnumerable<Pipe> GetPipeLoop(Pipe startPipe, IReadOnlyDictionary<Coordinate, Pipe> pipes)
    {
        var previous = startPipe;
        var current = pipes[startPipe.ConnectsTo.First()];
        var pipeLoop = new List<Pipe> { startPipe };
        
        while (!current.Location.Equals(startPipe.Location))
        {
            if (current.ConnectsTo.Contains(previous.Location))
            {
                pipeLoop.Add(current);
                
                var nextLocation = current.ConnectsTo.Single(c => !c.Equals(previous.Location));
                previous = current;
                current = pipes[nextLocation];
            }
            else
            {
                throw new ApplicationException("Current does not connect to previous");
            }
        }

        return pipeLoop;
    }

    public override int PartOne(IEnumerable<string> input)
    {
        var pipes = ParseInput(input, out var startPosition);
        UpdateWithStartPosition(pipes, startPosition);
        
        return GetPipeLoop(pipes[startPosition], pipes).Count()/2;
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        var pipes = ParseInput(input, out var startPosition);
        UpdateWithStartPosition(pipes, startPosition);

        var startPipe = pipes[startPosition];
        var pipeLoop = GetPipeLoop(startPipe, pipes).Select(pipe => pipe.Location).ToList();
        
        /*
         * By lack of a clear idea how to solve this problem programmatically, I started to look for a way
         * to solve this mathematically. During an hour of reading wikipedia I stumpbled upon Pick's Theorem
         * that relates the area of a polygon to the number of interior points and the number of boundary points
         *
         * Pick's Theorem : https://en.wikipedia.org/wiki/Pick%27s_theorem
         * 
         * The article also provides a way to calculate the area of a polygon by using the Shoelace formula.
         *
         * Shoelace formula : https://en.wikipedia.org/wiki/Shoelace_formula
         *
         * Since we know the number of boundary points (this is the number of pipes in the pipeloop) already,
         * Pick's Theorem gives us the number of interior points when we feed it the Area and de number of
         * boundary points.
         */
        
        var boundaryPairs = pipeLoop.Zip(pipeLoop.Skip(1)).ToList();
        boundaryPairs.Add((pipeLoop.Last(), startPosition));

        var determinants = boundaryPairs.Select(pair =>   
            pair.Item1.X * pair.Item2.Y - pair.Item1.Y * pair.Item2.X);

        // Shoelace formula
        var area = Math.Abs(determinants.Sum())/2;

        // Pick's theorem
        return area + 1 - pipeLoop.Count/2;
    }

    private class Pipe
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