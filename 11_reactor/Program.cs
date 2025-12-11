var input = File.ReadLines("input.txt");
//input = File.ReadLines("example.txt");

// Part 1
var map = ParseInput(input);
List<string> ordered = [];
HashSet<string> visited = [];
DFS("you");
Console.WriteLine(CountPaths("you", "out"));

// Part 2
//input = File.ReadLines("example2.txt");
map = ParseInput(input);
ordered = [];
visited = [];
DFS("svr");
Console.WriteLine(
    (CountPaths("svr", "fft") * CountPaths("fft", "dac") * CountPaths("dac", "out")) +
    (CountPaths("svr", "dac") * CountPaths("dac", "fft") * CountPaths("fft", "out")));

// Depth First Search
void DFS(string start)
{
    visited.Add(start);
    if (map.TryGetValue(start, out var targets))
    {
        foreach (var target in targets)
        {
            if (!visited.Contains(target))
            {
                DFS(target);
            }
        }
    }

    ordered.Add(start);
}

long CountPaths(string start, string end)
{
    Dictionary<string, long> paths = [];
    paths[start] = 1;
    foreach (var node in ordered.Reverse<string>())
    {
        if (map.TryGetValue(node, out var targets))
        {
            foreach (var target in targets)
            {
                if (!paths.ContainsKey(target))
                {
                    paths[target] = 0;
                }

                if (!paths.TryGetValue(node, out var value))
                {
                    value = 0;
                    paths[node] = value;
                }

                paths[target] += value;
            }
        }
    }

    Console.WriteLine($"Count Paths {start}-{end} : {paths[end]}");
    return paths[end];
}

Dictionary<string, string[]> ParseInput(IEnumerable<string> lines)
{
    return input.Select(line =>
    {
        var parts = line.Split(' ');
        var key = parts[0].Replace(":", "");
        var targets = parts[1..].ToArray();
        return (key, targets);
    }).ToDictionary();
}
