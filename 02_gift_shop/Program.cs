var input = File.ReadAllText("input.txt").Split(',');
// input = File.ReadAllText("example.txt").Split(',');
//input = ["998-1012"];

List<long> invalidIds = [];
foreach (var line in input)
{
    var parts = line.Split('-').Select(long.Parse).ToArray();
    var start = parts[0];
    var end = parts[1];

    // Part 1
    var invalid = CreateLongRange(start, end)
        .Select(i => i.ToString())
        .Where(id => id.Length % 2 == 0)
        .Where(id => id[..(id.Length / 2)] == id[(id.Length / 2)..])
        .Select(long.Parse)
        .ToList();

    // Part 2
    foreach (var id in CreateLongRange(start, end).Except(invalid).Select(id => id.ToString()).Where(i => i.Length > 1))
    {
        // Entire number is made up of same digit
        if (id.All(x => x == id[0]))
        {
            invalid.Add(long.Parse(id));
            continue;
        }

        // Check for recurring duplication
        for (int length = 2; length <= Math.Ceiling((double) id.Length / 2); length++)
        {
            var part = id[..length];
            var occurances = id.Split(part, StringSplitOptions.RemoveEmptyEntries);
            if (occurances.Length == 0)
            {
                invalid.Add(long.Parse(id));
            }
        }
    }

    invalidIds.AddRange(invalid);
    Console.WriteLine($"Range {start}-{end}: Found {(invalid.Count > 0 ? string.Join(", ", invalid) : "NO")} invalid IDs");
}

Console.WriteLine(invalidIds.Sum());

static IEnumerable<long> CreateLongRange(long start, long end)
{
    while (start <= end)
    {
        yield return start;
        start++;
    }
}

/*
    var rowPart = line[..7];
    var colPart = line[7..];
    int rowMin = 0;
    int rowMax = 127;
    foreach (var c in rowPart)
    {
        int mid = (rowMin + rowMax) / 2;
        if (c == 'F')
        {
            rowMax = mid;
        }
        else
        {
            rowMin = mid + 1;
        }
    }
    int row = rowMin;
    int colMin = 0;
    int colMax = 7;
    foreach (var c in colPart)
    {
        int mid = (colMin + colMax) / 2;
        if (c == 'L')
        {
            colMax = mid;
        }
        else
        {
            colMin = mid + 1;
        }
    }
    int col = colMin;
    int seatId = (row * 8) + col;
    invalidIds.Add(seatId);
*/
