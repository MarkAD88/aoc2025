using System.Linq;

var banks = File.ReadLines("input.txt");
// banks = File.ReadLines("example.txt");
//banks = ["3332444245543363143381512535862762325623248235232164657435326426367633324333853333536333733833357539"];


long totalPart1 = 0;
long totalPart2 = 0;
foreach (var bank in banks)
{
    // Part 1
    var batteries = bank.Select(c => int.Parse(c.ToString())).ToList();
    var first = batteries[..^1].Max();
    var firstIndex = batteries.IndexOf(first) + 1;
    var second = batteries[firstIndex..].Max();
    var secondIndex = batteries[firstIndex..].IndexOf(second);
    var valuePart1 = long.Parse($"{first}{second}");
    totalPart1 += valuePart1;

    // Part 2
    List<int> activeBatteries = [];
    List<int> remainingBatteries = [.. batteries];
    while (activeBatteries.Count < 12)
    {
        var max = remainingBatteries[..^(11 - activeBatteries.Count)].Max();
        var index = remainingBatteries[..^(11 - activeBatteries.Count)].IndexOf(max);
        activeBatteries.Add(max);
        remainingBatteries = remainingBatteries[(index + 1)..];

        // Console.WriteLine($"{bank}  {string.Join("", activeBatteries)}");
    }

    if (activeBatteries.Count < 12)
    {
        activeBatteries.AddRange(remainingBatteries);
    }

    var valuePart2 = long.Parse(string.Join("", activeBatteries));
    totalPart2 += valuePart2;
}

Console.WriteLine($"Total Part 1: {totalPart1}");
Console.WriteLine($"Total Part 2: {totalPart2}");
