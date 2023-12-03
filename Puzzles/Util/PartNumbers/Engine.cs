namespace AoC2023.Util.PartNumbers;

public class Engine
{
    private readonly IEnumerable<PartNumber> _partNumbers;
    private readonly IEnumerable<Coordinate> _symbolCoordinates;

    public Engine(IEnumerable<PartNumber> partNumbers, IEnumerable<Coordinate> symbols)
    {
        _partNumbers = partNumbers;
        _symbolCoordinates = symbols;
    }

    public IEnumerable<PartNumber> PartNumbers()
    {
        return _partNumbers;
    }

    public IEnumerable<Coordinate> SymbolCoordinates()
    {
        return _symbolCoordinates;
    }
}