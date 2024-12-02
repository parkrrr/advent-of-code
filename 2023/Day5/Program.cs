var lines = File.ReadAllLines("input.txt");


var seedToSoil = new Dictionary<long, long>();


for (var i = 0; i < lines.Length; i++)
{
    if (i == 0)
    {
        var seeds = lines[0].Replace("seeds: ", "").Split(" ").Select(long.Parse).ToArray();
        continue;
    }

    var mapName = lines[i];

    if (lines[i].Contains("map:"))
    {
        Console.WriteLine($"Mapping -- {lines[i]}");
        var end = 1;
        // new map
        while (!lines[i+ end].Contains("map:")) {
            end++;
        }

        var map = lines[(i+1)..(i+end-1)];

        foreach (var m in map)
        {
            var parts = m.Split(" ").Select(long.Parse).ToArray();
            
            for (long j = 0; j < parts[2]; j++)
            {
                seedToSoil.Add(parts[1] + j, parts[0] + j);
            }

        }

        i += end-1;
    }
}

void UnwrapMapping(Mapping mapping)
{
    for (long i = 0; i < mapping.Range; i++)
    {
        var next = mapping.SourceStart + i;
        var nextDestination = mapping.DestinationStart + i;
    }
}

record Mapping(long SourceStart, long DestinationStart, long Range);

public class Seed
{
    public long Id { get; set; }
    public long Soil { get; set; }
}