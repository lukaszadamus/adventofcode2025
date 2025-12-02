using System.Runtime.ExceptionServices;

ulong sum = 0;

static Range Parse(string[] range) => new(long.Parse(range[0]), long.Parse(range[1]));

void Validate(Range range)
{
    for (long i = range.Start; i <= range.End; i++)
    {
        var id = i.ToString();

        if (id.Length % 2 != 0)
        {
            continue;
        }

        if (id[0..(id.Length / 2)] == id[(id.Length / 2)..])
        {
            sum += (ulong)i;
        }
    }
}

void Validate2(Range range)
{
    for (long i = range.Start; i <= range.End; i++)
    {
        var id = i.ToString();

        var dividers = Dividers(id.Length);

        for(int d = 0; d < dividers.Length; d++)
        {
            var divider = dividers[d];
            var chunks = id.Chunk(divider).Select(x => new string(x)).ToArray();

            if(chunks.All(x => x == chunks[0]))
            {
                sum += (ulong)i;
                break;
            }
        }        
    }
}

int[] Dividers(int length)
{
     var dividers = new List<int>();

    for (int i = 1; i < length; i++)
    {
        if(i > length / 2)
        {
            break;
        }

        if (length % i == 0)
        {
            dividers.Add(i);
        }
    }

    dividers.Reverse();

    return [.. dividers];
}

File.ReadAllText("input.txt")
    .Split(',')
    .Select(x => x.Split('-'))
    .Select(Parse)
    .ToList()
    .ForEach(Validate2);

Console.WriteLine(sum);


record Range(long Start, long End);

