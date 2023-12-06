Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    int sum = 0;

    for (int line = 0; line < input.Length; line++)
    {
        int currentNum = 0;
        bool isPartNumber = false;
        for (int i = 0; i < input[line].Length; i++)
        {
            if (char.IsDigit(input[line][i]))
            {
                currentNum = currentNum * 10 + ToInt(input[line][i]);

                isPartNumber = isPartNumber ||
                    (line > 0 && i > 0 && IsSymbol(input[line - 1][i - 1])) ||
                    (line > 0 && IsSymbol(input[line - 1][i])) ||
                    (line > 0 && i < input[line].Length - 1 && IsSymbol(input[line - 1][i + 1])) ||
                    (i < input[line].Length - 1 && IsSymbol(input[line][i + 1])) ||
                    (line < input.Length - 1 && i < input[line].Length - 1 && IsSymbol(input[line + 1][i + 1])) ||
                    (line < input.Length - 1 && IsSymbol(input[line + 1][i])) ||
                    (line < input.Length - 1 && i > 0 && IsSymbol(input[line + 1][i - 1])) ||
                    (i > 0 && IsSymbol(input[line][i - 1]));
            }
            else
            {
                if (isPartNumber)
                    sum += currentNum;

                currentNum = 0;
                isPartNumber = false;
            }
        }

        if (isPartNumber)
            sum += currentNum;
    }

    Console.WriteLine(sum);

    static int ToInt(char c) => (int)c - '0';

    static bool IsSymbol(char c)
    {
        return !char.IsDigit(c) && c != '.';
    }
}

static void Solve2()
{
    var input = File.ReadAllLines("input.txt");

    var gears = new Dictionary<int, List<int>>();

    for (int line = 0; line < input.Length; line++)
    {
        int currentNum = 0;
        bool isGearNumber = false;
        int gearKey = 0;
        for (int i = 0; i < input[line].Length; i++)
        {
            if (char.IsDigit(input[line][i]))
            {
                currentNum = currentNum * 10 + ToInt(input[line][i]);

                if (isGearNumber)
                    continue;

                if (line > 0 && i > 0 && IsSymbol(input[line - 1][i - 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line - 1, i - 1);
                }
                else if (line > 0 && IsSymbol(input[line - 1][i]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line - 1, i);

                }
                else if (line > 0 && i < input[line].Length - 1 && IsSymbol(input[line - 1][i + 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line - 1, i + 1);

                }
                else if (i < input[line].Length - 1 && IsSymbol(input[line][i + 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line, i + 1);

                }
                else if (line < input.Length - 1 && i < input[line].Length - 1 && IsSymbol(input[line + 1][i + 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line + 1, i + 1);

                }
                else if (line < input.Length - 1 && IsSymbol(input[line + 1][i]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line + 1, i);

                }
                else if (line < input.Length - 1 && i > 0 && IsSymbol(input[line + 1][i - 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line + 1, i - 1);

                }
                else if (i > 0 && IsSymbol(input[line][i - 1]))
                {
                    isGearNumber = true;
                    gearKey = GetGearKey(line, i - 1);
                }
            }
            else
            {
                if (isGearNumber)
                    AddGearNumber(gears, gearKey, currentNum);

                currentNum = 0;
                isGearNumber = false;
            }
        }

        if (isGearNumber)
            AddGearNumber(gears, gearKey, currentNum);
    }

    int sum = gears.Values
        .Where(x => x.Count == 2)
        .Sum(x => x[0] * x[1]);

    Console.WriteLine(sum);

    static int ToInt(char c) => (int)c - '0';

    static bool IsSymbol(char c) => c == '*';

    static int GetGearKey(int line, int column) => line * 1000 + column;

    static void AddGearNumber(Dictionary<int, List<int>> gears, int gearKey, int number)
    {
        if (gears.ContainsKey(gearKey))
            gears[gearKey].Add(number);
        else
            gears[gearKey] = new List<int>() { number };
    }
}