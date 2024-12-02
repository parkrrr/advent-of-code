using System.ComponentModel.Design;

var lines = File.ReadAllLines("input.txt");



int GetGameId(string line)
{
    var q = line[..line.IndexOf(':')];
    var z = line[5..q.Length];
    return int.Parse(z);
}

List<Dictionary<string, int>> GetSubsets(string line)
{
    var start = line.IndexOf(':') + 1;
    var results = line[start..].Split(';');

    var colors = new[] { "red", "green", "blue" };
    var groups = new List<Dictionary<string, int>>();
    foreach (var result in results)
    {

        var dict = new Dictionary<string, int>();
        foreach (var color in colors)
        {
            var count = GetResult(color, result);
            dict[color] = count;
        }

        groups.Add(dict);
    }



    return groups;
}

int GetResult(string color, string result)
{
    var position = result.IndexOf(color);
    if (position >= 0)
    {


        var count = result[(position - 3)..position];
        return int.Parse(count);
    }

    return 0;
}

var sum = 0;
foreach (var line in lines)
{
    var id = GetGameId(line);
    var subsets = GetSubsets(line);

    var rounds = new List<Round>();
    foreach (var set in subsets)
    {
        var r = new Round(set["red"], set["green"], set["blue"]);
        rounds.Add(r);
    }

    var game = new Game(id, rounds);
    if (game.IsPossible())
    {
        Console.WriteLine($"Game {game.Id} is possible");
    }

    sum += game.GetPower();
}

Console.WriteLine(sum);

public class Round
{
    public readonly int Red;
    public readonly int Green;
    public readonly int Blue;

    public Round(int red, int green, int blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public bool IsPossible()
    {
        const int MaxRed = 12;
        const int MaxGreen = 13;
        const int MaxBlue = 14;

        return Red <= MaxRed && Green <= MaxGreen && Blue <= MaxBlue;
    }
}

public class Game
{
    public readonly int Id;
    private List<Round> Rounds;

    public Game(int id, IEnumerable<Round> rounds)
    {
        Id = id;
        Rounds = rounds.ToList();
    }

    public bool IsPossible()
    {
        return Rounds.All(r => r.IsPossible());
    }

    public int GetPower()
    {
        var blues = Rounds.Select(r => r.Blue).Max();
        var greens = Rounds.Select(r => r.Green).Max();
        var reds = Rounds.Select(r => r.Red).Max();

        return blues * greens * reds;
    }

}