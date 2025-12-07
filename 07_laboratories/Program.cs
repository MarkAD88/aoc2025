using System.Diagnostics;

var map = File.ReadAllLines("input.txt");
// map = File.ReadAllLines("example.txt");

string[] pathmap = map;

int splits = 0;
List<Beam> beams = [new(map[0].IndexOf('S'), 0)];
for (int y = 1; y < map.Length; y++)
{
    foreach (var beam in beams.Where(beam => beam.IsActive).ToArray())
    {
        beam.Y = y;
        if (map[beam.Y][beam.X] == '^')
        {
            splits += 1;
            beam.IsActive = false;
            beams.Add(new(beam.X - 1, beam.Y));
            beams.Add(new(beam.X + 1, beam.Y));
        }
    }

    // Clean up stacked beams
    beams = [.. beams.Distinct()];

    var line = pathmap[y];
    foreach (var beam in beams.Where(beam => beam.IsActive && beam.Y == y))
    {
        pathmap[y] = line = line.Remove(beam.X, 1).Insert(beam.X, "|");
    }

    Console.WriteLine(line);
}

Console.WriteLine(splits);

// Part 2
// Recursively call and cache per-starting-position results to avoid constant recalc
Dictionary<(int x, int y), long> cache = [];
Console.WriteLine(CountSplits(beams[0].X, beams[0].Y) + 1);
long CountSplits(int x, int y)
{
    if (cache.TryGetValue((x, y), out long count))
    {
        return count;
    }

    int startY = y;

    long result = 0;
    while (y < map.Length)
    {
        if (map[y][x] == '^')
        {
            result = 1 + CountSplits(x - 1, y) + CountSplits(x + 1, y);
            break;
        }
        y += 1;
    }

    cache.Add((x, startY), result);
    return result;
}

[DebuggerDisplay("(x={X}, y={Y})")]
class Beam
{
    public bool IsActive { get; set; } = true;
    public int X { get; set; }
    public int Y { get; set; }
    public Beam(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Beam other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        return false;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}