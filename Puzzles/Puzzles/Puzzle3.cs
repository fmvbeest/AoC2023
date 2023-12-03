using AoC2023.Util;

namespace AoC2023.Puzzles;

public class Puzzle3 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-03.txt";
    protected override string PuzzleTitle => "--- Day 3: Gear Ratios ---";
    
    public override int PartOne(IEnumerable<string> input)
    {
        var numbers = new List<(int, IEnumerable<Coordinate>)>();
        var symbols = new List<Coordinate>();

        var row = 0;

        foreach (var line in input)
        {
            var tmp = "";
            var tmpCoords = new List<Coordinate>();
            var index = 0;
            foreach (var c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    tmp += c;
                    tmpCoords.Add((index, row));
                }
                else
                {
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        numbers.Add((int.Parse(tmp), tmpCoords));
                        tmp = "";
                        tmpCoords = new List<Coordinate>();
                    }

                    if (c != '.')
                    {
                        symbols.Add((index, row));
                    }
                }
                index++;
            }

            if (!string.IsNullOrEmpty(tmp))
            {
                numbers.Add((int.Parse(tmp), tmpCoords));
            }
            row++;
        }
        
        var sum = 0;
        
        foreach (var (number, coordinates) in numbers)
        {
            var allNeighbours = new List<Coordinate>(); 
            foreach (var coordinate in coordinates)
            {
                allNeighbours.AddRange(coordinate.Neighbours());
            }

            if (allNeighbours.Any(neighbour => symbols.Contains(neighbour)))
            {
                sum += number;
            }
        }
        
        
        return sum;
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        var numbers = new List<(int, IEnumerable<Coordinate>)>();
        var symbols = new List<Coordinate>();
        
        var possibleGears = new List<Coordinate>();

        var row = 0;

        foreach (var line in input)
        {
            var tmp = "";
            var tmpCoords = new List<Coordinate>();
            var index = 0;
            foreach (var c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    tmp += c;
                    tmpCoords.Add((index, row));
                }
                else
                {
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        var number = int.Parse(tmp);
                        numbers.Add((number, tmpCoords));
                        tmp = "";
                        tmpCoords = new List<Coordinate>();
                    }

                    if (c != '.')
                    {
                        symbols.Add((index, row));
                        if (c == '*')
                        {
                            possibleGears.Add((index, row));
                        }
                    }

                    
                }
                index++;
            }

            if (!string.IsNullOrEmpty(tmp))
            {
                numbers.Add((int.Parse(tmp), tmpCoords));
            }
            row++;
        }

        var sum = 0;

        foreach (var possibleGear in possibleGears)
        {
            var gearNeighbours = possibleGear.Neighbours();

            var neighbourNumbers = new List<int>();
            
            foreach (var gearNeighbour in gearNeighbours)
            {
                var x = numbers.Where(tuple => tuple.Item2.Contains(gearNeighbour)).Select(tuple => tuple.Item1);

                neighbourNumbers.AddRange(x);
            }

            var y = neighbourNumbers.Distinct();
            if (y.Count() == 2)
            {
                var p = y.First() * y.Last();
                sum += p;
            }
        }
        
        return sum;
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}