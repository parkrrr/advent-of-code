using System.Diagnostics;
using System.Text.RegularExpressions;

const int a = 1; // 1 ms^2

var lines = File.ReadAllLines("input.txt");



var parseRegex = new Regex(@"\b\d+\b");

var timesMatches = parseRegex.Matches(lines[0]);
var distanceMatches = parseRegex.Matches(lines[1]);

Debug.Assert(timesMatches.Count == distanceMatches.Count);

var races = timesMatches.Count;
for (var i = 0; i < races; i++)
{
    Console.WriteLine($"Race {i + 1}");
    var raceDuration = int.Parse(timesMatches[i].Value);
    var recordDistance = int.Parse(distanceMatches[i].Value);
    
    
    for (var t = 0; t < raceDuration; t++)
    {
       // button held 
    }
    
}

int DistanceAtSpeed(int speed, int time)
{
    return speed * time;
}