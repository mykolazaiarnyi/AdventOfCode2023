Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach (var line in input)
    {
        var colonIndex = line.IndexOf(':');
        var nums = line.Substring(colonIndex + 1).Split('|');
        var winningNums = nums[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToHashSet();
        var yourNums = nums[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToHashSet();

        yourNums.IntersectWith(winningNums);
        sum += 1 << (yourNums.Count - 1);
    }

    Console.WriteLine(sum);
}


static void Solve2()
{
    var input = File.ReadAllLines("input.txt");
    var cardsCount = new int[input.Length];

    for (int i = 0; i < input.Length; i++)
    {
        var line = input[i];

        var colonIndex = line.IndexOf(':');
        var nums = line.Substring(colonIndex + 1).Split('|');
        var winningNums = nums[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToHashSet();
        var yourNums = nums[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToHashSet();

        yourNums.IntersectWith(winningNums);

        int winningCount = yourNums.Count;
        int currentCardsCount = cardsCount[i] + 1;
        for (int j = 1; j <= winningCount; j++)
        {
            cardsCount[i + j] += currentCardsCount;
        }
    }

    Console.WriteLine(cardsCount.Sum() + cardsCount.Length);
}