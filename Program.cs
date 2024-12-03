using System.Text.RegularExpressions;

var root = Environment.CurrentDirectory;

//December 1, Part 1
var pairs = File.ReadAllLines($@"{root}\input1.txt")
    .Select(line => line.Trim().Split(';'))
    .Select(pair => new { a = int.Parse(pair[0]), b = int.Parse(pair[1]) })
    .ToArray();
var left = pairs.Select(x => x.a).ToArray();
var right = pairs.Select(x => x.b).ToArray();

Array.Sort(left);
Array.Sort(right);
var r = left.Select((x,i)=>Math.Abs(right[i]-x)).Sum();
Console.WriteLine($"Diff: {r}" );

//December 1, Part 2
var rightCount = right.GroupBy(x=>x).ToDictionary(x=>x.Key, x=>x.Count());
var similarityScore = left.Select(x=>rightCount.TryGetValue(x, out var count) ? count*x : 0).Sum();
Console.WriteLine($"SimilarityScore: {similarityScore}" );

//December 2, Part 1
var reports =
    File.ReadAllLines($@"{root}\input2.txt")
        .Select(line => line.Trim().Split(' ').Select(int.Parse).ToArray())
        .ToArray();

var safeReports = reports.Where(IsReportSafe).Count();
Console.WriteLine($"Safe reports: {safeReports}" );

bool IsReportSafe(int[] report)
{
    var diffs = report.Take(report.Length - 1).Select((x, i) => report[i + 1] - x).ToArray();
    var ok1 = diffs.All(x => x is > 0 and <= 3);
    var ok2 = diffs.All(x => x is < 0 and >= -3);
    return ok1 || ok2;
}

//December 2, Part 2
var safeReportsWithDampener = reports.Where(IsReportSafeWithProblemDampener).Count();
Console.WriteLine($"Safe reports with problem dampener: {safeReportsWithDampener}" ); //561

bool IsReportSafeWithProblemDampener(int[] report)
{
    return IsReportSafe(report) || report.Select((_, i) => report.Take(i).Concat(report.Skip(i + 1)).ToArray()).Any(IsReportSafe);
}

//December 3
var nastyCode = File.ReadAllText($@"{root}\input3.txt");
var matches = new Regex(@"mul\((?<a>[0-9]+),(?<b>[0-9]+)\)")
    .Matches(nastyCode);

var multiplied = matches
    .Select(x=>int.Parse(x.Groups["a"].Value) * int.Parse(x.Groups["b"].Value))
    .Sum();

Console.WriteLine($"Sum: {multiplied}" );  //170807108

//December 3
var matchesOnOff = new Regex(@"mul\((?<a>[0-9]+),(?<b>[0-9]+)\)|don\'t\(\)|do\(\)");
var enabled = true;
var sum = 0;
foreach (Match match in matchesOnOff.Matches(nastyCode))
{
    if (match.Value == "do()")
        enabled = true;
    else if (match.Value == "don't()")
        enabled = false;
    else if (enabled)
        sum += int.Parse(match.Groups["a"].Value) * int.Parse(match.Groups["b"].Value);
}
Console.WriteLine($"Sum with do and don't: {sum}" );
