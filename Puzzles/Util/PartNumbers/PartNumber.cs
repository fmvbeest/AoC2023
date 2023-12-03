namespace AoC2023.Util.PartNumbers;

public class PartNumber
{
    public int Number { get; set; }
    private readonly IEnumerable<Coordinate> _coordinates;
        
    public PartNumber(IEnumerable<Coordinate> coordinates)
    {
        _coordinates = coordinates;
    }

    public IEnumerable<Coordinate> Coordinates()
    {
        return _coordinates;
    }
}