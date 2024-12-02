using System.Text.RegularExpressions;

var cardRegex = new Regex("Card (\\d+):");

var lines = File.ReadAllLines("input.txt");

var scratchers = new Dictionary<int, Scratcher>(lines.Length);

var sum = 0;
foreach (var line in lines)
{
    var card = GetCardNumber(line);
    var winningNumbers = GetWinningNumbers(line);
    var playedNumbers = GetPlayedNumbers(line);

    var matchedNumbers = winningNumbers.Intersect(playedNumbers).ToList();

    var score = 0;

    if (matchedNumbers.Any())
    {
        var pointsPerWinner = 1;
        for (var z = matchedNumbers.Count - 1; z > 0; z--)
        {
            pointsPerWinner *= 2;
        }

        score = pointsPerWinner;
    }

    sum += score;

    var scratcher = new Scratcher(card, matchedNumbers, score);
    scratchers.Add(card, scratcher);

    Console.WriteLine($"Card {card} is worth {score} points");
}


// part 2

var instances = new Dictionary<int, int>();
foreach (var instance in scratchers)
{
    instances.Add(instance.Key, 1);
}

var queue = new Queue<Scratcher>(scratchers.Values);
while (queue.TryDequeue(out var scratcher))
{
    var winners = scratcher.MatchedNumbers.Count;

    for (var i = 0; i < scratcher.MatchedNumbers.Count; i++)
    {
        var newScratcher = scratchers[scratcher.CardNumber + 1 + i];
        instances[newScratcher.CardNumber]++;
        queue.Enqueue(newScratcher);
    }
}

var sum2 = 0;
foreach (var instance in instances)
{
    Console.WriteLine($"{instance.Key} > {instance.Value}");
    sum2 += instance.Value;
}


Console.WriteLine($"Answer: {sum2}");

// funcs

static int GetCardNumber(string line)
{
    var start = line.IndexOf("Card ") + 5;
    var end = line.IndexOf(':');
    var cardNumber = line[start..end];
    return int.Parse(cardNumber);
}

static List<int> GetWinningNumbers(string line)
{
    var start = line.IndexOf(':') + 2;
    var end = line.IndexOf('|') - 1;
    var numbers = line[start..end];
    var split = numbers.Split(' ');
    return ParseNumbers(split);
}

static List<int> GetPlayedNumbers(string line)
{
    var start = line.IndexOf('|') + 2;
    var numbers = line[start..];
    var split = numbers.Split(' ');
    return ParseNumbers(split);
}

static List<int> ParseNumbers(IEnumerable<string> strings)
{
    return strings.Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
}

record Scratcher(int CardNumber, List<int> MatchedNumbers, int Score);
