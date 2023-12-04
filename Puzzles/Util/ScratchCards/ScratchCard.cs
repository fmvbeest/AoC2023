namespace AoC2023.Util.ScratchCards;

public class ScratchCard
{
    public int Id { get; init; }
    public int MatchingNumberCount { get; }

    public ScratchCard(IEnumerable<int> winningNumbers, IEnumerable<int> cardNumbers)
    {
        var winningNumbers1 = winningNumbers.ToArray();
        var cardNumbers1 = cardNumbers.ToArray();

        MatchingNumberCount = winningNumbers1.Intersect(cardNumbers1).Count();
    }
}