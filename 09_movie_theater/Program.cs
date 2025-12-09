using System.Drawing;
using System.Text;

var input = File.ReadAllLines("input.txt");
// input = File.ReadAllLines("example.txt");

var tiles = input.Select(line =>
{
    var parts = line.Split(',').Select(int.Parse).ToArray();
    return new Point(parts[0], parts[1]);
}).ToList();

// find largest rectangle
List<(Point A, Point B, long Area)> areas = [];
for (int a = 0; a < tiles.Count; a++)
{
    for (int b = a + 1; b < tiles.Count; b++)
    {
        var width = Math.Abs(tiles[a].X - tiles[b].X);
        var height = Math.Abs(tiles[a].Y - tiles[b].Y);
        var area = ((long) width + 1) * ((long) height + 1);
        areas.Add((tiles[a], tiles[b], area));
    }
}

Console.WriteLine($"Part 1: {areas.MaxBy(x => x.Area)}");

// Part 2
// find largest rectangle
// that is fully contained by existing rectangles

// Determin the range of valid X values for every Y value
List<Point> validTiles = [.. tiles];
List<int> unterminatedColumns = [];
Dictionary<int, (int Min, int Max)> validColumns = [];

for (int y = tiles.Min(tile => tile.Y); y <= tiles.Max(tile => tile.Y); y++)
{
    var columns = tiles
        .Where(tile => tile.Y == y)
        .Select(tile => tile.X)
        .OrderBy(x => x)
        .ToList();

    // Working columns
    var workingColumns = columns.Union(unterminatedColumns);

    validColumns.Add(y, (workingColumns.Min(), workingColumns.Max()));

    // Terminate columns that have run out
    if (unterminatedColumns.Count == 0)
    {
        unterminatedColumns = columns;
    }
    else if (columns.Count > 0)
    {
        // Terminate any column that appears in both sets
        // Add any columns that appeared only in the columns list
        var newColumns = columns.Except(unterminatedColumns);
        unterminatedColumns = [.. newColumns, .. unterminatedColumns.Except(columns)];
    }
}

// Run the area check to find the largest rectangle
// that falls entirely within valid tiles.
foreach (var area in areas.OrderByDescending(a => a.Area))
{
    // Check to make sure all area arrives in list
    var minX = Math.Min(area.A.X, area.B.X);
    var minY = Math.Min(area.A.Y, area.B.Y);
    var maxX = Math.Max(area.A.X, area.B.X);
    var maxY = Math.Max(area.A.Y, area.B.Y);

    bool isValid = true;
    for (int y = minY; y <= maxY && isValid; y++)
    {
        isValid = validColumns.TryGetValue(y, out var range)
            && minX >= range.Min
            && maxX <= range.Max;
    }

    if (isValid)
    {
        Console.WriteLine($"Part 2: {area}");
        break;
    }
}
