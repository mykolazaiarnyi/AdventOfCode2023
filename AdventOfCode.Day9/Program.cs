Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach (var line in input)
    {
        var nums = line.Split(' ').Select(x => int.Parse(x)).ToList();
        var diffs = new List<int>(nums.Count - 1);
        int lastsSum = 0;
        int last = nums[^1];

        bool allZeros = false;

        while (!allZeros)
        {
            allZeros = true;
            for (int i = 0; i < nums.Count - 1; i++)
            {
                diffs.Add(nums[i + 1] - nums[i]);
                allZeros &= diffs[i] == 0;
            }

            lastsSum += diffs[^1];

            (nums, diffs) = (diffs, nums);

            diffs.Clear();
        }

        sum += last + lastsSum;
    }

    Console.WriteLine(sum);
}

static void Solve2()
{
    var input = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach (var line in input)
    {
        var nums = line.Split(' ').Select(x => int.Parse(x)).ToList();
        var diffs = new List<int>(nums.Count - 1);
        int firstsSum = nums[0];

        bool allZeros = false;
        int sign = -1;
        while (!allZeros)
        {
            allZeros = true;
            for (int i = 0; i < nums.Count - 1; i++)
            {
                diffs.Add(nums[i + 1] - nums[i]);
                allZeros &= diffs[i] == 0;
            }

            firstsSum += sign * diffs[0];
            sign *= -1;

            (nums, diffs) = (diffs, nums);

            diffs.Clear();
        }

        sum += firstsSum;
    }

    Console.WriteLine(sum);
}
