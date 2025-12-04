using System.Drawing;

var map = File.ReadAllLines("input.txt");
//map = File.ReadAllLines("example.txt");

int resultPart1 = 0;
for (var y = 0; y < map.Length; y++)
{
    for (var x = 0; x < map[y].Length; x++)
    {
        char cell = map[y][x];
        if (cell != '@')
        {
            continue;
        }

        if (CountRollsAroundCoordinate(map, x, y) <= 3)
        {
            resultPart1 += 1;
        }
    }
}

Console.WriteLine(resultPart1);

int resultPart2 = 0;
string[] modifiedMap = map;
int? rollsRemoved = null;
while (rollsRemoved != 0)
{
    map = modifiedMap;
    rollsRemoved = 0;
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            char cell = map[y][x];
            if (cell != '@')
            {
                continue;
            }

            if (CountRollsAroundCoordinate(map, x, y) <= 3)
            {
                resultPart2 += 1;
                modifiedMap[y] = map[y].Remove(x, 1).Insert(x, " ");
                rollsRemoved += 1;
            }
        }
    }
}

Console.WriteLine(resultPart2);

int CountRollsAroundCoordinate(string[] map, int x, int y)
{
    int left = Math.Max(x - 1, 0);
    int right = Math.Min(x + 1, map[y].Length - 1);
    int top = Math.Max(y - 1, 0);
    int bottom = Math.Min(y + 1, map.Length - 1);

    var rows = map[top..(bottom + 1)];
    var cells = rows.SelectMany(row => row[left..(right + 1)]).ToArray();
    return cells.Count(cell => cell == '@') - 1;
}