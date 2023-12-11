Solve2();

static void Solve1()
{
    var input = File.ReadAllLines("input.txt");

    var seeds = input[0]
        .Substring(7)
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();

    int i = 3;

    var seedToSoilMap = ParseMap(input, ref i);
    i += 2;
    var soilToFertilizerMap = ParseMap(input, ref i);
    i += 2;
    var fertilizerToWaterMap = ParseMap(input, ref i);
    i += 2;
    var waterToLightMap = ParseMap(input, ref i);
    i += 2;
    var lightToTemperatureMap = ParseMap(input, ref i);
    i += 2;
    var temperatureToHumidityMap = ParseMap(input, ref i);
    i += 2;
    var humidityToLocationMap = ParseMap(input, ref i);

    var minLocation = seeds.Select(x => seedToSoilMap.GetDestinationNumber(x))
        .Select(x => soilToFertilizerMap.GetDestinationNumber(x))
        .Select(x => fertilizerToWaterMap.GetDestinationNumber(x))
        .Select(x => waterToLightMap.GetDestinationNumber(x))
        .Select(x => lightToTemperatureMap.GetDestinationNumber(x))
        .Select(x => temperatureToHumidityMap.GetDestinationNumber(x))
        .Select(x => humidityToLocationMap.GetDestinationNumber(x))
        .Min();

    Console.WriteLine(minLocation);
}

// Very slow and will take most of your RAM, but works
static void Solve2()
{
    var input = File.ReadAllLines("input.txt");

    var seedRanges = input[0]
        .Substring(7)
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();

    var seeds = new List<long[]>();

    for (int j = 0; j < seedRanges.Length; j += 2)
    {
        var seedRange = Enumerable.Range(0, (int)seedRanges[j + 1])
            .Select(x => seedRanges[j] + x)
            .ToArray();

        seeds.Add(seedRange);
    }

    int i = 3;

    var seedToSoilMap = ParseMap(input, ref i);
    i += 2;
    var soilToFertilizerMap = ParseMap(input, ref i);
    i += 2;
    var fertilizerToWaterMap = ParseMap(input, ref i);
    i += 2;
    var waterToLightMap = ParseMap(input, ref i);
    i += 2;
    var lightToTemperatureMap = ParseMap(input, ref i);
    i += 2;
    var temperatureToHumidityMap = ParseMap(input, ref i);
    i += 2;
    var humidityToLocationMap = ParseMap(input, ref i);

    var minLocation = seeds.Select(s => s
        .Select(x => seedToSoilMap.GetDestinationNumber(x))
        .Select(x => soilToFertilizerMap.GetDestinationNumber(x))
        .Select(x => fertilizerToWaterMap.GetDestinationNumber(x))
        .Select(x => waterToLightMap.GetDestinationNumber(x))
        .Select(x => lightToTemperatureMap.GetDestinationNumber(x))
        .Select(x => temperatureToHumidityMap.GetDestinationNumber(x))
        .Select(x => humidityToLocationMap.GetDestinationNumber(x))
        .Min())
        .Min();

    Console.WriteLine(minLocation);
}

static RangeMap ParseMap(string[] input, ref int i)
{
    var map = new RangeMap();

    for (; i < input.Length && !string.IsNullOrWhiteSpace(input[i]); i++)
    {
        var nums = input[i]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => long.Parse(x))
            .ToArray();

        var range = new Range
        {
            DestinationRangeStart = nums[0],
            SourceRangeStart = nums[1],
            RangeLength = nums[2]
        };

        map.Add(range);
    }

    return map;
}

class Range
{
    public long SourceRangeStart { get; set; }
    public long DestinationRangeStart { get; set; }
    public long RangeLength { get; set; }
}

class RangeMap
{
    private SortedList<long, Range> _list = new();

    public void Add(Range range)
    {
        _list.Add(range.SourceRangeStart, range);
    }

    public long GetDestinationNumber(long sourceNumber)
    {
        var keys = _list.Keys;
        int i = 0;
        for (; i < keys.Count; i++)
        {
            if (keys[i] > sourceNumber)
                break;
        }

        if (i == 0)
            return sourceNumber;

        var range = _list.Values[i - 1];

        if (sourceNumber < range.SourceRangeStart + range.RangeLength)
        {
            long offset = sourceNumber - range.SourceRangeStart;
            return range.DestinationRangeStart + offset;
        }

        return sourceNumber;
    }
}
