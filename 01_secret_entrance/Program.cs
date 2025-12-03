var input = File.ReadLines("input.txt");
// input = File.ReadLines("example.txt");

bool pointerStartedAtZero = false;
int zeroClicks = 0;
int zeroCount = 0;
int pointer = 50;
foreach (var line in input)
{
    var direction = line[0] == 'L' ? -1 : 1;
    var distance = int.Parse(line[1..]);
    pointer += (direction * distance);

    // Part 1
    if (pointer % 100 == 0)
    {
        zeroCount++;
    }

    // Part 2
    {
        // Calculate times passed zero
        zeroClicks += Math.Abs(pointer) / 100;

        // Handle negative cross overs
        // Handle exact zero
        if ((pointer < 0 && !pointerStartedAtZero) || pointer == 0)
        {
            zeroClicks++;
        }

        // Move pointer in bounds
        pointer %= 100;

        // Reset flag
        pointerStartedAtZero = pointer == 0;

        // Handle wrapping
        if (pointer < 0)
        {
            pointer += 100;
        }
    }

    Console.WriteLine($"{line[0]} {distance,4} {pointer,3} {zeroCount,4} {zeroClicks,4}");
}

Console.WriteLine();
Console.WriteLine($"ZeroCount: {zeroCount}");
Console.WriteLine($"ZeroClick: {zeroClicks}");
