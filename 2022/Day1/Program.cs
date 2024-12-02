using System.Globalization;

var input = await File.ReadAllLinesAsync("input.txt");

var elves = new List<int>();

var acc = 0;

foreach (var line in input)
{
    if (line == "")
    {
        elves.Add(acc);
        acc = 0;
        continue;
    }

    var num = int.Parse(line);
    acc += num;
}

elves.Sort();
elves.Reverse();

var best = elves[0] + elves[1] + elves[2];

Console.WriteLine(best);