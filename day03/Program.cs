var sum = File.ReadAllLines("input.txt")
    .Select(x => x.ToArray())
    .Aggregate(new List<int[]>(), (acc, line) =>
    {
        acc.Add([.. line.Select(c => c - '0')]);
        return acc;
    })
    .Aggregate(0L, (sum, bank) => sum + MaxJoltage(bank, 12));

Console.WriteLine(sum);

long MaxJoltage(int[] bank, int numberOfBatteries = 2)
{
    var batteries = new List<int>();
    var start = 0;
    var end = bank.Length - numberOfBatteries;

    for (var i = 0; i < numberOfBatteries; i++)
    {
        var (max, index) = FindMaxOnRight(start, end);
        batteries.Add(max);

        start = index + 1;
        end++;
    }

    return long.Parse(string.Join("", batteries));

    (int max, int index) FindMaxOnRight(int start, int end)
    {
        var max = -1;
        var index = -1;

        for (var i = start; i <= end; i++)
        {
            if (bank[i] > max)
            {
                max = bank[i];
                index = i;
            }
        }

        return (max, index);
    }
}
