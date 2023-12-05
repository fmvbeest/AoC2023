namespace AoC2023.Util.Fertilizer;

public class MapRange
{
    public long Start { get; set; }
    public long End { get; set; }
        
    public long Offset { get; set; }

    public bool InRange(long x)
    {
        return x >= Start && x <= End;
    }
}