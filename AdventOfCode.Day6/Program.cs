Solve2();

void Solve1()
{
    var input = File.ReadAllLines("input.txt");
    var times = input[0][5..]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToArray();
    var distances = input[1][9..]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToArray();

    var winCasesCounts = new List<int>();

    for (int i = 0; i < times.Length; i++)
    {
        var t = times[i];
        var r = distances[i];

        var d = t * t - 4 * r;

        if (d <= 0)
            continue;

        var x1 = (t - Math.Sqrt(d)) / 2;
        var x2 = (t + Math.Sqrt(d)) / 2;

        int count = (int)(Math.Floor(Math.Max(x1, x2)) - Math.Ceiling(Math.Min(x1, x2))) + 1;

        count -= IsInt(x1) ? 2 : 0;

        winCasesCounts.Add(count);
    }

    Console.WriteLine(winCasesCounts.Aggregate(1, (acc, next) => acc * next, acc => acc));
}

void Solve2()
{
    var input = File.ReadAllLines("input.txt");
    var t = long.Parse(input[0][5..].Replace(" ", ""));
    var r = long.Parse(input[1][9..].Replace(" ", ""));

    var d = t * t - 4 * r;

    var x1 = (t - Math.Sqrt(d)) / 2;
    var x2 = (t + Math.Sqrt(d)) / 2;

    long count = (long)(Math.Floor(Math.Max(x1, x2)) - Math.Ceiling(Math.Min(x1, x2))) + 1;

    count -= IsInt(x1) ? 2 : 0;

    Console.WriteLine(count);
}

bool IsInt(double d) => d - (long)d < double.Epsilon;