var lines = await File.ReadAllLinesAsync("input.txt");

var x = 1;

var stack = new Stack<int>();

var acc = 0;
var count = 0;
foreach (var line in lines)
{
    if (line == "noop")
    {

    }

    var command = line.Split(' ');

    if (count % 2 == 0)
    {

    }
}