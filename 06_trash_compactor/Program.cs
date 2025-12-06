var input = File.ReadLines("input.txt");
//input = File.ReadLines("example.txt");

long resultPart1 = 0;
List<List<long>> equations = [];
foreach (var line in input)
{
    if (line.ContainsAny('*', '+'))
    {
        var operators = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
        for (int x = 0; x < operators.Length; x++)
        {
            if (operators[x] == "*")
            {
                long resultMult = equations[x].Aggregate(1L, (acc, val) => acc * val);
                resultPart1 += resultMult;
            }
            else if (operators[x] == "+")
            {
                resultPart1 += equations[x].Sum();
            }
        }

        break;
    }

    var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
    for (int x = 0; x < numbers.Length; x++)
    {
        if (equations.Count < x + 1)
        {
            equations.Add([]);
        }

        equations[x].Add(numbers[x]);
    }
}

Console.WriteLine(resultPart1);

// Part 2 (honestly this is just evil)
long resultPart2 = 0;
equations.Clear();
var lines = input.ToArray();
var numberLines = lines.Length - 1;
List<long> equation = [];
for (int x = lines.Max(line => line.Length) - 1; x >= -1; x--)
{
    var numberString = x >= 0
        ? new string([.. lines[..^1].Select(line => line[x])])
        : string.Empty;
    if (string.IsNullOrWhiteSpace(numberString))
    {
        // read operator from last line
        var op = lines.Last()[x + 1];
        if (op == '*')
        {
            long sum = equation.Aggregate(1L, (acc, val) => acc * val);
            resultPart2 += sum;
            Console.WriteLine($"{string.Join(" * ", equation)} = {sum}");
        }
        else if (op == '+')
        {
            long sum = equation.Sum();
            resultPart2 += sum;
            Console.WriteLine($"{string.Join(" + ", equation)} = {sum}");
        }
        else
        {
            throw new Exception($"Unknown operator: {op}");
        }

        equation.Clear();
        continue;
    }

    var number = long.Parse(numberString!);
    equation.Add(number);
}

Console.WriteLine(resultPart2);
