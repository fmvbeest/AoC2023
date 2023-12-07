using AoC2023.Util.CamelCards;

namespace AoC2023.Puzzles;

public class Puzzle7 : PuzzleBase<IEnumerable<string>, int, int>
{
    protected override string Filename => "Input/puzzle-input-07.txt";
    protected override string PuzzleTitle => "--- Day 7: Camel Cards ---";

    private static CamelType ParseHandType(string hand, bool jokerRules = false)
    {
        var groups = hand.GroupBy(c => c).ToList();

        if (!jokerRules)
            return groups.Count switch
            {
                1 => CamelType.FiveOfAKind,
                5 => CamelType.HighCard,
                4 => CamelType.OnePair,
                2 when groups.Any(g => g.Count() == 4) => CamelType.FourOfAKind,
                2 when groups.Any(g => g.Count() == 3) => CamelType.FullHouse,
                3 when groups.Any(g => g.Count() == 3) => CamelType.ThreeOfAKind,
                _ => CamelType.TwoPair
            };
        
        return groups.Count switch
        {
            1 => CamelType.FiveOfAKind,
            5 => hand.Contains('J') ? CamelType.OnePair : CamelType.HighCard,
            4 => hand.Contains('J') ? CamelType.ThreeOfAKind : CamelType.OnePair,
            2 when groups.Any(g => g.Count() == 4) => 
                hand.Contains('J') ? CamelType.FiveOfAKind : CamelType.FourOfAKind,
            2 when groups.Any(g => g.Count() == 3) => 
                hand.Contains('J') ? CamelType.FiveOfAKind : CamelType.FullHouse,
            3 when groups.Any(g => g.Count() == 3) => 
                hand.Contains('J') ? CamelType.FourOfAKind : CamelType.ThreeOfAKind,
            _ => hand.Count(c => c == 'J') switch
            {
                2 => CamelType.FourOfAKind,
                1 => CamelType.FullHouse,
                _ => CamelType.TwoPair
            }
        };
    }

    private static CamelHand ParseCamelHand(string line, bool jokerRules = false)
    {
        var data = line.Split(' ');

        return new CamelHand(data[0], ParseHandType(data[0], jokerRules), jokerRules) 
            { Bid = int.Parse(data[1]) };
    }
    
    public override int PartOne(IEnumerable<string> input)
    {
        var camelHands = input.Select(line => ParseCamelHand(line)).ToList();

        camelHands.Sort();
        
        return camelHands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
    }
    
    public override int PartTwo(IEnumerable<string> input)
    {
        var camelHands = input.Select(line => ParseCamelHand(line, jokerRules:true)).ToList();

        camelHands.Sort();
        
        return camelHands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
    }
    
    public override IEnumerable<string> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetAllLines();
    }
}