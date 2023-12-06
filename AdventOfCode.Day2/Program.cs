Solve2();

static void Solve1()
{
    const int MaxRedNum = 12, MaxGreenNum = 13, MaxBlueNum = 14;

    var input = File.ReadAllLines("input.txt");
    long sum = 0;

    foreach (var game in input)
    {
        const int GameIdStart = 5;
        var g = game.AsSpan()[GameIdStart..];
        var colonIndex = g.IndexOf(':');
        var gameId = int.Parse(g[..colonIndex]);

        var maxCubesCounts = new Dictionary<char, int>()
    {
        { 'r', 0 },
        { 'g', 0 },
        { 'b', 0 }
    };

        g = g.Slice(colonIndex + 2);
        while (g.Length > 0)
        {
            ReadOnlySpan<char> draw = default;

            int semicolonIndex = g.IndexOf(";");
            if (semicolonIndex >= 0)
            {
                draw = g[..semicolonIndex];
                g = g[(semicolonIndex + 2)..];
            }
            else
            {
                draw = g;
                g = default;
            }

            int i = 0;
            while (i < draw.Length)
            {
                int numberLength = 0;
                while (char.IsDigit(draw[i]))
                {
                    numberLength++;
                    i++;
                }
                int number = int.Parse(draw.Slice(i - numberLength, numberLength));

                i++;
                maxCubesCounts[draw[i]] = Math.Max(maxCubesCounts[draw[i]], number);

                i += draw[i] switch
                {
                    'r' => "red, ".Length,
                    'g' => "green, ".Length,
                    'b' => "blue, ".Length
                };
            }
        }

        if (maxCubesCounts['r'] <= MaxRedNum
            && maxCubesCounts['g'] <= MaxGreenNum
            && maxCubesCounts['b'] <= MaxBlueNum)
            sum += gameId;
    }

    Console.WriteLine(sum);
}

static void Solve2()
{
    var input = File.ReadAllLines("input.txt");
    int sum = 0;

    foreach (var game in input)
    {
        const int GameIdStart = 5;
        var g = game.AsSpan()[GameIdStart..];
        var colonIndex = g.IndexOf(':');
        var gameId = int.Parse(g[..colonIndex]);

        var maxCubesCounts = new Dictionary<char, int>()
        {
            { 'r', 0 },
            { 'g', 0 },
            { 'b', 0 }
        };

        g = g.Slice(colonIndex + 2);
        while (g.Length > 0)
        {
            ReadOnlySpan<char> draw = default;

            int semicolonIndex = g.IndexOf(";");
            if (semicolonIndex >= 0)
            {
                draw = g[..semicolonIndex];
                g = g[(semicolonIndex + 2)..];
            }
            else
            {
                draw = g;
                g = default;
            }

            int i = 0;
            while (i < draw.Length)
            {
                int numberLength = 0;
                while (char.IsDigit(draw[i]))
                {
                    numberLength++;
                    i++;
                }
                int number = int.Parse(draw.Slice(i - numberLength, numberLength));

                i++;
                maxCubesCounts[draw[i]] = Math.Max(maxCubesCounts[draw[i]], number);

                i += draw[i] switch
                {
                    'r' => "red, ".Length,
                    'g' => "green, ".Length,
                    'b' => "blue, ".Length
                };
            }
        }

        sum += maxCubesCounts['r'] * maxCubesCounts['g'] * maxCubesCounts['b'];
    }

    Console.WriteLine(sum);
}