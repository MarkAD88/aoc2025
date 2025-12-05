var input = File.ReadLines("input.txt");
// input = File.ReadLines("example.txt");

List<SimpleRange> freshIngredients = [];
List<long> availableIngredients = [];

foreach (var line in input)
{
    if (line == string.Empty)
    {
        continue;
    }

    if (line.Contains('-'))
    {
        var parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries).Select(part => long.Parse(part)).ToArray();
        freshIngredients.AddRange(new SimpleRange(parts[0], parts[1]));
    }
    else
    {
        availableIngredients.Add(long.Parse(line));
    }
}

int freshCount = 0;
foreach (var ingredient in availableIngredients)
{
    if (freshIngredients.Any(fi => fi.Start <= ingredient && fi.End >= ingredient))
    {
        freshCount += 1;
    }
}

Console.WriteLine(freshCount);

// Part 2

// Order fresh items by their min
var sorted = freshIngredients.OrderBy(fi => fi.Start).ToList();

// Start finding total number of items in each range
// Keep track of our "MAX" pointer
long part2Count = 0;
long max = 0;

foreach (var range in sorted)
{
    if (range.End <= max)
        continue;

    long start = Math.Max(range.Start, max + 1);
    long end = Math.Max(range.End, max);

    part2Count += end - start + 1;

    Console.WriteLine($"{range} : {start}-{end} : {part2Count}");

    max = end;
}

Console.WriteLine(part2Count);

static IEnumerable<long> CreateLongRange(long start, long end)
{
    while (start <= end)
    {
        yield return start;
        start++;
    }
}

class SimpleRange(long start, long end)
{
    public long Start { get; set; } = start;

    public long End { get; set; } = end;

    public override string ToString() => $"{Start}-{End}";
}