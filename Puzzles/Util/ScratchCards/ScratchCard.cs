namespace AoC2023.Util.ScratchCards;

public class ScratchCard
{
    public int Id { get; init; }

    private readonly int[] _winningNumbers;
    private readonly int[] _cardNumbers;

    public ScratchCard(IEnumerable<int> winningNumbers, IEnumerable<int> cardNumbers)
    {
        _winningNumbers = winningNumbers.ToArray();
        _cardNumbers = cardNumbers.ToArray();
    }

    public IEnumerable<int> WinningNumbers()
    {
        return _winningNumbers;
    }
        
    public IEnumerable<int> CardNumbers()
    {
        return _cardNumbers;
    }
}