Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    var hands = new List<Hand1>();
    var handToBid = new Dictionary<string, int>();

    foreach (var line in input)
    {
        var l = line.Split(' ');
        var hand = new Hand1(l[0]);
        var bid = int.Parse(l[1]);

        handToBid[l[0]] = bid;
        hands.Add(hand);
    }

    hands.Sort();

    int sum = 0;
    for (int i = 0; i < hands.Count; i++)
    {
        var hand = hands[i];
        var bid = handToBid[hand.HandString];
        sum += bid * (i + 1);
    }

    Console.WriteLine(sum);
}

static void Solve2()
{
    var input = File.ReadAllLines("input.txt");

    var hands = new List<Hand2>();
    var handToBid = new Dictionary<string, int>();

    foreach (var line in input)
    {
        var l = line.Split(' ');
        var hand = new Hand2(l[0]);
        var bid = int.Parse(l[1]);

        handToBid[l[0]] = bid;
        hands.Add(hand);
    }

    hands.Sort();

    int sum = 0;
    for (int i = 0; i < hands.Count; i++)
    {
        var hand = hands[i];
        var bid = handToBid[hand.HandString];
        sum += bid * (i + 1);
    }

    Console.WriteLine(sum);
}

enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfKind,
    FullHouse,
    FourOfKind,
    FiveOfKind
}

class Hand1 : IComparable<Hand1>
{
    private static readonly Dictionary<char, int> _cardsValues = new Dictionary<char, int>()
    {
        { '2', 1 },
        { '3', 2 },
        { '4', 3 },
        { '5', 4 },
        { '6', 5 },
        { '7', 6 },
        { '8', 7 },
        { '9', 8 },
        { 'T', 9 },
        { 'J', 10 },
        { 'Q', 11 },
        { 'K', 12 },
        { 'A', 13 },
    };

    public HandType HandType { get; }

    public string HandString => _hand;

    private readonly string _hand;

    public Hand1(string hand)
    {
        _hand = hand;
        HandType = CalculateHandType(hand);
    }

    private static HandType CalculateHandType(string hand)
    {
        var cards = hand.ToLookup(k => k).ToDictionary(x => x.Key, x => x.Count());

        if (cards.Count == 1)
            return HandType.FiveOfKind;
        else if (cards.Count == 2 && cards.Values.Any(x => x == 4))
            return HandType.FourOfKind;
        else if (cards.Count == 2 && cards.Values.Any(x => x == 3))
            return HandType.FullHouse;
        else if (cards.Count == 3 && cards.Values.Any(x => x == 3))
            return HandType.ThreeOfKind;
        else if (cards.Values.Count(x => x == 2) == 2)
            return HandType.TwoPair;
        else if (cards.Values.Count(x => x == 2) == 1)
            return HandType.OnePair;
        else
            return HandType.HighCard;
    }

    public int CompareTo(Hand1? other)
    {
        if (HandType != other!.HandType)
            return HandType - other!.HandType;

        for (int i = 0; i < _hand.Length; i++)
        {
            var diff = CompareCards(_hand[i], other._hand[i]);
            if (diff != 0)
                return diff;
        }

        return 0;
    }

    private int CompareCards(char c1, char c2) => _cardsValues[c1] - _cardsValues[c2];
}

class Hand2 : IComparable<Hand2>
{
    private static readonly Dictionary<char, int> _cardsValues = new Dictionary<char, int>()
    {
        { 'J', 0 },
        { '2', 1 },
        { '3', 2 },
        { '4', 3 },
        { '5', 4 },
        { '6', 5 },
        { '7', 6 },
        { '8', 7 },
        { '9', 8 },
        { 'T', 9 },
        { 'Q', 11 },
        { 'K', 12 },
        { 'A', 13 },
    };

    public HandType HandType { get; }

    public string HandString => _hand;

    private readonly string _hand;

    public Hand2(string hand)
    {
        _hand = hand;
        HandType = CalculateHandType(hand);
    }

    private static HandType CalculateHandType(string hand)
    {
        var cards = hand.Where(x => x != 'J')
            .ToLookup(k => k)
            .ToDictionary(x => x.Key, x => x.Count());

        var jCount = hand.Count(x => x == 'J');

        if (cards.Count == 1 || jCount == 5)
            return HandType.FiveOfKind;
        else if (cards.Values.Any(x => x + jCount == 4))
            return HandType.FourOfKind;
        else if (cards.Values.Any(x => x + jCount == 3) && cards.Count == 2)
            return HandType.FullHouse;
        else if (cards.Values.Any(x => x + jCount == 3))
            return HandType.ThreeOfKind;
        else if (cards.Values.Count(x => x == 2) == 2)
            return HandType.TwoPair;
        else if (cards.Values.Any(x => x + jCount == 2))
            return HandType.OnePair;
        else
            return HandType.HighCard;
    }

    public int CompareTo(Hand2? other)
    {
        if (HandType != other!.HandType)
            return HandType - other!.HandType;

        for (int i = 0; i < _hand.Length; i++)
        {
            var diff = CompareCards(_hand[i], other._hand[i]);
            if (diff != 0)
                return diff;
        }

        return 0;
    }

    private int CompareCards(char c1, char c2) => _cardsValues[c1] - _cardsValues[c2];
}