using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

var lines = await File.ReadAllLinesAsync("input.txt");

var parseRegex = new Regex(@"\d+|[A-Z][A-Z]");

var valves = new List<Valve>();


var vd = new Dictionary<string, Valve>();
foreach (var line in lines)
{
    var match = parseRegex.Matches(line);

    var name = match[0].Value;
    var flow = int.Parse(match[1].Value);

    var children = new List<string>();
    for (int i = 2; i < match.Count; i++)
    {
        children.Add(match[i].Value);
    }

    var valve = new Valve(name, flow, children);
    vd.Add(name, valve);
}

foreach (var v in vd)
{
    var name = v.Key;
    var valve = v.Value;
    Console.WriteLine(valve._childrenSpec);
    foreach (var k in valve._childrenSpec)
    {
        valve.Children.Add(vd[k]);
    }
}

var aa = vd["AA"];
var x = aa.Children;


var currentValve = aa;
for (var minute = 0; minute < 30; minute++)
{
    


}

class Valve
{
    public readonly IEnumerable<string> _childrenSpec;

    public string Name { get; }
    public int FlowRate { get; }
    public List<Valve> Children { get; }

    public bool Open { get; set; }

    public Valve(string name, int flowRate, IEnumerable<string> childrenSpec)
    {
        Children = new List<Valve>();

        Name = name;
        FlowRate = flowRate;
        _childrenSpec = childrenSpec;
        Open = false;
    }
}

