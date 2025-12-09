using System.Drawing;
using System.Numerics;
using System.Text;

var input = File.ReadAllLines("input.txt");
input = File.ReadAllLines("example.txt");

var tiles = input.Select(line =>
{
    var parts = line.Split(',').Select(int.Parse).ToArray();
    return new Vector2(parts[0], parts[1]);
}).ToList();

// find largest rectangle
List<(Vector2 A, Vector2 B, long Area)> areas = [];
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
