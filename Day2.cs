using System.Text.RegularExpressions;

public class Day2
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;

        //December 2, Part 1
        var reports =
            File.ReadAllLines($@"{root}\input2.txt")
                .Select(line => line.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();

        var safeReports = reports.Where(IsReportSafe).Count();
        Console.WriteLine($"Safe reports: {safeReports}");

        bool IsReportSafe(int[] report)
        {
            var diffs = report.Take(report.Length - 1).Select((x, i) => report[i + 1] - x).ToArray();
            var ok1 = diffs.All(x => x is > 0 and <= 3);
            var ok2 = diffs.All(x => x is < 0 and >= -3);
            return ok1 || ok2;
        }

        //December 2, Part 2
        var safeReportsWithDampener = reports.Where(IsReportSafeWithProblemDampener).Count();
        Console.WriteLine($"Safe reports with problem dampener: {safeReportsWithDampener}"); //561

        bool IsReportSafeWithProblemDampener(int[] report)
        {
            return IsReportSafe(report) || report.Select((_, i) => report.Take(i).Concat(report.Skip(i + 1)).ToArray())
                .Any(IsReportSafe);
        }
    }
}