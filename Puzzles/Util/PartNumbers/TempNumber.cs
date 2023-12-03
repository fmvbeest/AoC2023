namespace AoC2023.Util.PartNumbers;

public class TempPartNumber
{
    private string _number = string.Empty;
    private readonly List<Coordinate> _coordinates = new();

    public void Update(char digit, Coordinate coordinate)
    {
        _number += digit;
        _coordinates.Add(coordinate);
    }

    public PartNumber Finish()
    {
        var partNumber = new PartNumber(_coordinates.ToList()) { Number = int.Parse(_number) }; 
        _number = string.Empty;
        _coordinates.Clear();
        return partNumber;
    }

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(_number);
    }
}