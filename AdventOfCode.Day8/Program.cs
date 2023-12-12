Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    var instructions = input[0];

    var map = new Dictionary<string, (string Left, string Right)>();

    for (int i = 2; i < input.Length; i++)
    {
        var curr = input[i][..3];
        var left = input[i].Substring(7, 3);
        var right = input[i].Substring(12, 3);
        map[curr] = (left, right);
    }

    var current = "AAA";
    int steps = 0;
    while (current != "ZZZ")
    {
        char instruction = instructions[steps % instructions.Length];

        if (instruction == 'L')
            current = map[current].Left;
        else
            current = map[current].Right;

        steps++;
    }

    Console.WriteLine(steps);
}

static void Solve2()
{
    var input = File.ReadAllLines("input.txt");

    var instructions = input[0];

    var map = new Dictionary<string, (string Left, string Right)>();

    var currents = new List<string>();

    for (int i = 2; i < input.Length; i++)
    {
        var curr = input[i][..3];
        var left = input[i].Substring(7, 3);
        var right = input[i].Substring(12, 3);
        map[curr] = (left, right);

        if (curr[2] == 'A')
            currents.Add(curr);
    }

    int steps = 0;
    var periods = new long[currents.Count];
    int periodsFound = 0;

    while (periodsFound < periods.Length)
    {
        char instruction = instructions[steps % instructions.Length];

        for (int i = 0; i < currents.Count; i++)
        {
            if (instruction == 'L')
                currents[i] = map[currents[i]].Left;
            else
                currents[i] = map[currents[i]].Right;

            if (currents[i][2] == 'Z' && periods[i] == 0)
            {
                periodsFound++;
                periods[i] = steps + 1;
            }

        }

        steps++;
    }

    Console.WriteLine(LCM(periods));

    static long GCD(long n1, long n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return GCD(n2, n1 % n2);
        }
    }

    static long LCM(long[] numbers)
    {
        return numbers.Aggregate((S, val) => S * val / GCD(S, val));
    }
}