using System.Numerics;

var input = File.ReadLines("input.txt");
var connectionLimit = 1000;

// input = File.ReadLines("example.txt");
// connectionLimit = 10;

var boxes = input.Select(line =>
{
    var parts = line.Split(',').Select(int.Parse).ToArray();
    Vector box = new Vector(parts[0], parts[1], parts[2]);
    return box;
}).ToList();

// Get distances
List<(double Distance, Vector A, Vector B)> boxDistances = [];
for (int outter = 0; outter < boxes.Count; outter++)
{
    for (int inner = outter + 1; inner < boxes.Count; inner++)
    {
        var a = boxes[outter];
        var b = boxes[inner];
        boxDistances.Add((Vector.Distance(a, b), a, b));
    }
}

// Now find the {x} shortest connections and build circuits
int connections = 0;
List<HashSet<Vector>> circuits = [];
foreach (var (distance, a, b) in boxDistances.OrderBy(d => d.Distance))
{
    Console.WriteLine($"Processing {distance} {a} {b}");

    // Find existing circuits
    var matches = circuits.Where(c => c.Contains(a) || c.Contains(b)).ToArray();
    if (matches.Length == 0)
    {
        circuits.Add([a, b]);
        boxes.Remove(a);
        boxes.Remove(b);
        connections++;
    }
    else if (matches.Length == 1)
    {
        if (!matches[0].Add(a) && !matches[0].Add(b))
            System.Diagnostics.Debug.Assert(true, "WTF?!");

        boxes.Remove(a);
        boxes.Remove(b);
        connections++;
    }
    else if (matches.Length == 2)
    {
        circuits.Remove(matches[0]);
        circuits.Remove(matches[1]);
        circuits.Add(new([.. matches[0], .. matches[1]]));
        connections++;
        Console.WriteLine("Merging circuits.... ");
    }

    int counts = circuits.Sum(c => c.Count);
    Console.WriteLine($"{counts} {connections}");

    if (connections >= connectionLimit)
        break;
}

var part1 = circuits
    .OrderByDescending(x => x.Count)
    .Take(3)
    .Select(x => x.Count)
    .Aggregate(1L, (acc, val) => acc * val);
Console.WriteLine(part1);

// Part 2

// Add all remaining boxes to circuit list as orphaned circuits
circuits.AddRange(boxes.Select(b => new HashSet<Vector>([b])));

// Calculate the distance from every circuit to every other circuit
List<(double Distance, HashSet<Vector> A, HashSet<Vector> B)> circuitDistances = [];
for (int outter = 0; outter < circuits.Count; outter++)
{
    for (int inner = outter + 1; inner < circuits.Count; inner++)
    {
        var outterCircuit = circuits[outter];
        var innerCircuit = circuits[inner];

        var minDistance = outterCircuit.SelectMany(ocb => innerCircuit.Select(icb => Vector.Distance(ocb, icb))).Min();
        circuitDistances.Add((minDistance, outterCircuit, innerCircuit));
    }
}

circuitDistances = [.. circuitDistances.OrderBy(d => d.Distance)];

Console.WriteLine("part2");

record Vector(int X, int Y, int Z)
{
    public static double Distance(Vector a, Vector b)
    {
        return Math.Sqrt(
            Math.Pow(Math.Abs(a.X - b.X), 2) +
            Math.Pow(Math.Abs(a.Y - b.Y), 2) +
            Math.Pow(Math.Abs(a.Z - b.Z), 2));
    }
}