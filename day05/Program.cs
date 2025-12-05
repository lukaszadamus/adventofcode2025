InputData ParseInput()
{
    var lines = File.ReadAllLines("input.txt");
    var ranges = new List<Range>();
    var ids = new List<long>();

    var parsingRanges = true;

    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            parsingRanges = false;
            continue;
        }

        if (parsingRanges)
        {
            var parts = line.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            ranges.Add(new Range(start, end));
        }
        else
        {
            var id = long.Parse(line);
            ids.Add(id);
        }
    }

    ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

    var mergedRanges = new List<Range>();
    foreach (var range in ranges)
    {
        if (mergedRanges.Count == 0)
        {
            mergedRanges.Add(range);
            continue;
        }

        var lastRange = mergedRanges[^1];
        if (lastRange.End >= range.Start)
        {
            var newRange = new Range(lastRange.Start, Math.Max(lastRange.End, range.End));
            mergedRanges[^1] = newRange;
        }
        else
        {
            mergedRanges.Add(range);
        }
    }

    ids.Sort();

    var minStart = mergedRanges.Min(r => r.Start);
    var maxEnd = mergedRanges.Max(r => r.End);

    return new InputData(mergedRanges, ids, minStart, maxEnd);
}

long Solve1(InputData input) => input.Ids.Aggregate(0L, (acc, id) =>
{
    if (id < input.MinStart || id > input.MaxEnd)
        return acc;

    foreach (var range in input.Ranges)
    {
        if (id >= range.Start && id <= range.End)
        {
            acc++;
        }
    }

    return acc;
});


long Solve2(InputData inputData) => inputData.Ranges.Aggregate(0L, (acc, range) =>
{
    acc += range.End - range.Start + 1;
    return acc;
});

var input = ParseInput();

Console.WriteLine(Solve1(input));
Console.WriteLine(Solve2(input));

record InputData(List<Range> Ranges, List<long> Ids, long MinStart, long MaxEnd);
record Range(long Start, long End);