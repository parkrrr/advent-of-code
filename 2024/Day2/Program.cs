// Compares two numbers and checks if the difference is greater than 1 and less than 3
var IsWithinSafeRange = new Func<int, int, bool>((curr, prev) => Math.Abs(curr - prev) is >= 1 and <= 3);

// Checks if the current number is greater than the previous number
var IsIncreasing = new Func<int, int, bool>((curr, prev) => curr > prev);

// Checks if the current number is less than the previous number
var IsDecreasing = new Func<int, int, bool>((curr, prev) => curr < prev);

// Takes in a whole record, and confirms if all elements adhere to the comparison function and levels are within the defined safe range
var IsMovingSafely = new Func<IEnumerable<int>, Func<int, int, bool>, bool>((report, comparison) => 
        report.Skip(1) // skip the first record because we need it for `Zip`
        .Zip(report,
            (curr, prev) => comparison(curr, prev) && IsWithinSafeRange(curr, prev) // run the conditions against each pair
        ) 
        .All(zip => zip == true)); // all conditions must return true

var input = File.ReadAllLines("input.txt");

// convet everything to integers
var reports = input.Select(report => report.Split(' ').Select(int.Parse));

// count the number of reports where each level is either entirely increasing in a safe range or entirely decreasing in a safe range
var safe = reports.Where(report => IsMovingSafely(report, IsIncreasing) || IsMovingSafely(report, IsDecreasing));

var safeCount = safe.Count();

var @unsafe = reports.Except(safe);

var stillSafe = 0;
foreach (var report in @unsafe)
{
    var iterations = report.Count();

    for (var i = 0; i < iterations; i++)
    {
        // just test every permutation until it works
        // this only works because the input is relatively small
        // this would not scale as the number of levels in each report increases
        var cutReport = report.Where((_, index) => index != i);

        var result = IsMovingSafely(cutReport, IsIncreasing) || IsMovingSafely(cutReport, IsDecreasing);

        if (result)
        {
            stillSafe++;
            break;
        }
    }
}

Console.WriteLine(safe.Count());
Console.WriteLine(stillSafe);
