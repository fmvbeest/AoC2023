using AoC2023.Util;
using AoC2023.Util.PartNumbers;

namespace AoC2023.Puzzles;

public class Puzzle3 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-03.txt";
    protected override string PuzzleTitle => "--- Day 3: Gear Ratios ---";

    private static Engine ParseInput(IEnumerable<string> input, int part = 1)
    {
        var numbers = new List<PartNumber>();
        var symbols = new List<Coordinate>();
        
        var tempNumber = new TempPartNumber();
        var row = 0;
        foreach (var line in input)
        {
            var index = 0;
            foreach (var c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    tempNumber.Update(c, (index, row));
                    index++;
                    continue;
                }
                if (!tempNumber.IsEmpty())
                {
                    numbers.Add(tempNumber.Finish());
                }

                if (c != '.')
                {
                    if (part != 1 && c != '*')
                    {
                        continue;
                    }
                    symbols.Add((index, row));
                }
                index++;
            }

            if (!tempNumber.IsEmpty())
            {
                numbers.Add(tempNumber.Finish());
            }
            row++;
        }

        return new Engine(numbers, symbols);
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        var engine = ParseInput(input);
        
        var sum = 0;
        
        foreach (var partNumber in engine.PartNumbers())
        {
            var allNeighbours = new List<Coordinate>(); 
            foreach (var coordinate in partNumber.Coordinates())
            {
                allNeighbours.AddRange(coordinate.Neighbours());
            }

            if (allNeighbours.Distinct().Any(neighbour => engine.SymbolCoordinates().Contains(neighbour)))
            {
                sum += partNumber.Number;
            }
        }
        
        return sum;
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        var engine = ParseInput(input);

        var sum = 0;

        foreach (var gear in engine.SymbolCoordinates())
        {
            var neighbourNumbers = new List<int>();
            
            foreach (var gearNeighbour in gear.Neighbours())
            {
                neighbourNumbers.AddRange(engine.PartNumbers()
                    .Where(p => p.Coordinates().Contains(gearNeighbour))
                    .Select(p => p.Number));
            }

            var gearNumbers = neighbourNumbers.Distinct().ToArray();
            if (gearNumbers.Length == 2)
            {
                sum += gearNumbers[0] * gearNumbers[1];
            }
        }
        
        return sum;
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}