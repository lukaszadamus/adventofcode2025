char[] Neighbors(int y, int x, char[,] grid)
{
    var neighbors = new List<char>();

    for (int dy = -1; dy <= 1; dy++)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            if (dy == 0 && dx == 0)
                continue;

            int newY = y + dy;
            int newX = x + dx;

            if (newY >= 0 && newY < grid.GetLength(0) &&
                newX >= 0 && newX < grid.GetLength(1))
            {
                neighbors.Add(grid[newY, newX]);
            }
        }
    }
    return [.. neighbors];
}

Position[] FindRemovableRolls(char[,] grid)
{
    var removable = new List<Position>();
    for (var y = 0; y < grid.GetLength(0); y++)
    {
        for (var x = 0; x < grid.GetLength(1); x++)
        {
            if (grid[y, x] != '@')
                continue;

            var rolls = Neighbors(y, x, grid).Where(c => c == '@').Count();
            if (rolls < 4)
            {
                removable.Add(new Position(y, x));
            }
        }
    }
    return [.. removable];
}

int Process(char[,] grid, int rounds = 1)
{
    int removed = 0;

    while (true)
    {
        var toRemove = FindRemovableRolls(grid);

        removed += toRemove.Length;

        if (toRemove.Length == 0)
            break;

        foreach (var pos in toRemove)
        {
            grid[pos.Y, pos.X] = '.';
        }

        if (rounds > 0)
        {
            rounds--;
            if (rounds == 0)
                break;
        }
    }

    return removed;
}

char[,] ReadInput()
{
    var lines = File.ReadAllLines("input.txt");
    int rows = lines.Length;
    int cols = lines[0].Length;
    char[,] grid = new char[rows, cols];

    for (int r = 0; r < rows; r++)
    {
        for (int c = 0; c < cols; c++)
        {
            grid[r, c] = lines[r][c];
        }
    }

    return grid;
}

Console.WriteLine($"A: {Process(ReadInput())}");
Console.WriteLine($"B: {Process(ReadInput(), rounds: -1)}");

record Position(int Y, int X);