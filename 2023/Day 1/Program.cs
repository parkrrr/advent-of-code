var lines = await File.ReadAllLinesAsync("input.txt");

var numbers = new Dictionary<string, int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

var sum = 0;
foreach (var line in lines)
{
    var compiledLine = string.Empty;

    for (var i = 0; i < line.Length; i++)
    {
        var chr = line[i];

        // number finding tree
        if (char.IsNumber(chr))
        {
            compiledLine += chr;
        }
        else if (chr == 'o')
        {
            if (line.Length - i < 2)
            {
                continue;
            }

            if (line[i + 1] == 'n')
            {
                compiledLine += '1';
            }
        }
        else if (chr == 't')
        {
            if (line[i + 1] == 'w')
            {
                compiledLine += '2';
                i += 1;
            }
            else if (line[i + 1] == 'h')
            {
                compiledLine += '3';
                i += 3;
            }
        }
        else if (chr == 'f')
        {
            if (line[i + 1] == 'o')
            {
                compiledLine += '4';
                i += 2;
            }
            else if (line[i + 1] == 'i')
            {
                compiledLine += '5';
                i += 2;
            }
        }
        else if (chr == 's')
        {
            if (line[i + 1] == 'i')
            {
                compiledLine += '6';
                i += 1;
            }
            else if (line[i + 1] == 'e')
            {
                compiledLine += '7';
                i += 3;
            }
        }
        else if (chr == 'e')
        {
            compiledLine += '8';
            i += 3;
        }
        else if (chr == 'n')
        {
            if (line.Length - i < 3)
            {
                continue;
            }

            if (line[i + 1] == 'i')
            {
                compiledLine += '9';
                i += 2;
            }

        }

    }

    string formedNumber;
    if (compiledLine.Length == 1)
    {
        formedNumber = $"{compiledLine.First()}{compiledLine.First()}";
    }
    else
    {
        formedNumber = $"{compiledLine.First()}{compiledLine.Last()}";
    }

    var numeric = int.Parse(formedNumber);
    Console.WriteLine(numeric);

    sum += numeric;
}

Console.WriteLine($"Answer: {sum}");