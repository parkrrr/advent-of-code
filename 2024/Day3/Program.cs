using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var regex = new Regex("mul\\((\\d*),(\\d*)\\)|(don\\'t\\(\\))|(do\\(\\))");

var accum = 0;

var enabled = true;
foreach (var memory in input)
{
    var instances = regex.Matches(memory);

    foreach (Match instance in instances)
    {
        if (instance.Groups[0].Value == "don't()")
        {
            enabled = false;
        }
        else if (instance.Groups[0].Value == "do()")
        {
            enabled = true;
        }
        else
        {
            if (!enabled)
            {
                continue;
            }

            var a = int.Parse(instance.Groups[1].Value);
            var b = int.Parse(instance.Groups[2].Value);

            var result = a * b;

            accum += result;
        }
    }
}

Console.WriteLine(accum);