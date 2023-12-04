using AoC2023.Util.ScratchCards;

namespace AoC2023.Puzzles;

public class Puzzle4 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-04.txt";
    protected override string PuzzleTitle => "--- Day 4: Scratchcards ---";

    private static int ParseCard(string card)
    {
        var parts = card.Split(':')[1].Trim();
        var winningNumbers = parts.Split('|')[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        var cardNumbers = parts.Split('|')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

        var hits = winningNumbers.Count(number => cardNumbers.Contains(number));
        
        return hits == 0 ? 0 : Convert.ToInt32(Math.Pow(2, hits - 1));
    }
    
    private static int ParseCards(IEnumerable<string> input)
    {
        var cardData = input.ToArray();
        var cards = new Dictionary<int, ScratchCard>();

        foreach (var card in cardData)
        {
            var data = card.Split(':');
            var cardNumber = int.Parse(data[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            var parts = data[1].Trim();
            var winningNumbers = parts.Split('|')[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var ownNumbers = parts.Split('|')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

            var parsedCard = new ScratchCard(winningNumbers, ownNumbers) { Id = cardNumber };
            cards.Add(parsedCard.Id, parsedCard);
        }

        var cardNumbers = new List<int>();

        var toCheck = new List<ScratchCard>();
        
        foreach (var (cardNumber, card) in cards)
        {
            cardNumbers.Add(cardNumber);
            var hits = card.WinningNumbers().Count(winningNumber => card.CardNumbers().Contains(winningNumber));

            if (hits <= 0) continue;
            
            var newCardNumbers = Enumerable.Range(cardNumber + 1, hits);
            toCheck.AddRange(newCardNumbers.Select(number => cards[number]));
        }

        while (toCheck.Any())
        {
            var cardsToCheck = toCheck.ToList();
            toCheck.Clear();
            
            foreach (var card in cardsToCheck)
            {
                cardNumbers.Add(card.Id);
                var hits = card.WinningNumbers().Count(winningNumber => card.CardNumbers().Contains(winningNumber));

                if (hits <= 0) continue;
                
                var newCardNumbers = Enumerable.Range(card.Id + 1, hits);
                toCheck.AddRange(newCardNumbers.Select(number => cards[number]));
            }
        }

        return cardNumbers.Count;
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        return input.Sum(ParseCard);
    }

    public override int PartTwo(IEnumerable<string> input)
    {
        return ParseCards(input);
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}