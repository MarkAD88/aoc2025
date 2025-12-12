var input = File.ReadAllLines("input.txt");
//input = File.ReadAllLines("example.txt");

List<Shape> shapes = [];
List<Region> regions = [];
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    if (line == string.Empty)
    {
        continue;
    }

    if (line[1] == ':')
    {
        shapes.Add(Shape.Parse(input.Skip(i + 1).Take(3)));
        continue;
    }

    if (line.Contains('x'))
    {
        regions.Add(Region.Parse(line));
    }
}

int doable = 0;
foreach (var region in regions)
{
    int totalRegionSize = region.Width * region.Length;
    int totalPresentSize = region.Quantities.Select((count, index) => shapes[index].Size * count).Sum();
    // Console.WriteLine($"{totalRegionSize} == {totalPresentSize}  ----- {(totalPresentSize > totalRegionSize ? "FAIL" : "POSSIBLE")}");

    if (totalPresentSize * 1.25 < totalRegionSize)
    {
        doable += 1;
    }
    else if (totalPresentSize > totalRegionSize)
    {
        Console.WriteLine($"Impossible ---> Total Present Size > Total Region Size : {totalPresentSize} > {totalRegionSize}");
    }
    else
    {
        Console.WriteLine($"MAYBE ---> {totalPresentSize} < {totalRegionSize}");
    }
}

Console.WriteLine(doable);

class Shape()
{
    public static Shape Parse(IEnumerable<string> lines)
    {
        return new Shape { Lines = [.. lines] };
    }

    public string[] Lines { get; set; } = [];

    public int Size => Lines.Select(line => line.Count(c => c == '#')).Sum();
}

class Region()
{
    public static Region Parse(string line)
    {
        var parts = line.Replace(":", "").Replace("x", " ").Split(' ');
        return new()
        {
            Width = int.Parse(parts[0]),
            Length = int.Parse(parts[1]),
            Quantities = [.. parts[2..].Select(int.Parse)]
        };
    }

    public int Width { get; set; }

    public int Length { get; set; }

    public int[] Quantities { get; set; } = [];
}