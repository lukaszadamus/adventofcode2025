using System.ComponentModel.DataAnnotations.Schema;


var state = new State();

File.ReadAllLines("input.txt")
    .Select(line => new Op
    {
        Direction = line[0],
        Distance = int.Parse(line[1..])
    })
    .ToList()
    .ForEach(op => Rotate(state, op));

Console.WriteLine($"zeros: {state.NumberOfZeros}");

static void Rotate(State state, Op op)
{
    var normalized = op.Distance % state.Base;
    var p = state.Base - state.CurrentPosition;
    var l = state.Base - p;

    var zeros = op.Distance / state.Base;

    if (op.Direction == 'R')
    {
        if (normalized < p)
        {
            state.CurrentPosition += normalized;
        }
        else
        {
            state.CurrentPosition = normalized - p;
            if (state.CurrentPosition != 0)
            {
                zeros++;
            }
        }
    }
    else if (op.Direction == 'L')
    {
        if (normalized <= l)
        {
            state.CurrentPosition -= normalized;
        }
        else
        {
            var newPosition = state.CurrentPosition + state.Base - normalized;
            if (state.CurrentPosition != 0 && newPosition != 0)
            {
                zeros++;
            }
            state.CurrentPosition = newPosition;
        }
    }

    state.NumberOfZeros += zeros;

    if (state.CurrentPosition == 0)
    {
        state.NumberOfZeros++;
    }
}

class Op
{
    public char Direction { get; set; }
    public int Distance { get; set; }

    public override string ToString()
    {
        return $"{Direction}{Distance}";
    }
}

class State
{
    public int Base { get; set; } = 100;
    public int CurrentPosition { get; set; } = 50;
    public int NumberOfZeros { get; set; } = 0;
}
