using AoC2023.Util.ScratchCards;

namespace AoC2023.Puzzles;

public class Puzzle4 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-04.txt";
    protected override string PuzzleTitle => "--- Day 4: Scratchcards ---";

    private static ScratchCard ParseCard(string cardData)
    {
        var data = cardData.Split(':');
        var cardNumber = int.Parse(data[0].Replace("Card", string.Empty).Trim());
        var numbers = data[1].Split('|');
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        var ownNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        
        return new ScratchCard(winningNumbers, ownNumbers) { Id = cardNumber };
    }
    
    private static int CalculateCardScore(string card)
    {
        var parsedCard = ParseCard(card);
        
        return parsedCard.MatchingNumberCount == 0 ? 0 
            : Convert.ToInt32(Math.Pow(2, parsedCard.MatchingNumberCount - 1));
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(CalculateCardScore);
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        var cardResults = input.ToArray().Select(ParseCard)
            .ToDictionary(card => card.Id, 
                card => Enumerable.Range(card.Id + 1, card.MatchingNumberCount).ToArray());;
        
        var numEvaluatedCards = cardResults.Keys.Count;
        var cardsToCheck = cardResults.Keys.ToList();

        while (cardsToCheck.Any())
        {
            var toCheck = cardsToCheck.ToList();
            cardsToCheck.Clear();

            foreach (var cardId in toCheck)
            {
                if (cardResults.TryGetValue(cardId, out var newCards))
                {
                    cardsToCheck.AddRange(newCards);
                    numEvaluatedCards += newCards.Length;
                }
            }
        }

        return numEvaluatedCards;
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}