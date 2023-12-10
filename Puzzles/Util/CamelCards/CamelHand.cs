namespace AoC2023.Util.CamelCards;

public class CamelHand : IComparable<CamelHand>
{
    public int Bid { get; init; }

    private readonly bool _jokerRules;

    private readonly HandType _type;

    private readonly string _hand;

    public CamelHand(string hand, HandType type, bool jokerRules = false)
    {
        _hand = hand;
        _type = type;
        _jokerRules = jokerRules;
    }
    
    private int CardValue(char c)
    {
        return c switch
        {
            >= '2' and <= '9' => int.Parse(c.ToString()),
            'T' => 10,
            'J' => _jokerRules ? 1 : 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => -1
        };
    }
    
    public int CompareTo(CamelHand? other)
    {
        if (other == null)
        {
            return -1;
        }
    
        if (_type != other._type)
        {
            return _type.CompareTo(other._type);
        }
    
        for (var i = 0; i < _hand.Length; i++)
        {
            var x = CardValue(_hand[i]);
            var y = CardValue(other._hand[i]);
            if (x != y)
            {
                return x.CompareTo(y);
            }
        }
    
        return 0;
    }
}