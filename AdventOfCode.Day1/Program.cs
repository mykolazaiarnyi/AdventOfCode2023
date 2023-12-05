Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");
    int sum = 0;
    foreach (var line in input)
    {
        int? first = null, last = null;
        foreach (var c in line)
        {
            if (char.IsDigit(c))
            {
                var digit = int.Parse(c.ToString());
                first ??= digit;
                last = digit;
            }
        }
        sum += first.Value * 10 + last.Value;
    }

    Console.WriteLine(sum);
}


static void Solve2()
{

    var input = File.ReadAllLines("input.txt");
    int sum = 0;
    foreach (var line in input)
    {
        int? first = null, last = null;

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (char.IsDigit(c))
            {
                var digit = int.Parse(c.ToString());
                first ??= digit;
                last = digit;
            }
            else
            {
                var s = line.AsSpan().Slice(i);
                int? digit = null;
                if (s.StartsWith("one"))
                    digit = 1;
                else if (s.StartsWith("two"))
                    digit = 2;
                else if (s.StartsWith("three"))
                    digit = 3;
                else if (s.StartsWith("four"))
                    digit = 4;
                else if (s.StartsWith("five"))
                    digit = 5;
                else if (s.StartsWith("six"))
                    digit = 6;
                else if (s.StartsWith("seven"))
                    digit = 7;
                else if (s.StartsWith("eight"))
                    digit = 8;
                else if (s.StartsWith("nine"))
                    digit = 9;

                if (digit.HasValue)
                {
                    first ??= digit;
                    last = digit;
                }
            }
        }

        sum += first.Value * 10 + last.Value;
    }

    Console.WriteLine(sum);
}
