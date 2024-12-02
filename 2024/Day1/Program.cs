var input = await File.ReadAllLinesAsync("input.txt");

List<int> column1 = [];
List<int> column2 = [];

foreach (var row in input)
{
    var values = row.Split("   ");
    column1.Add(int.Parse(values[0].Trim()));
    column2.Add(int.Parse(values[1].Trim()));
}

column1.Sort();
column2.Sort();

int sum = 0;
int similarity = 0;
for (var i = 0; i < column1.Count; i++)
{
    var entry = column1[i];
    sum += Math.Abs(entry - column2[i]);

    similarity += entry * column2.Count(c => c == entry);
}

Console.WriteLine(sum);
Console.WriteLine(similarity);