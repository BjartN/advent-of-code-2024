using System.Text.RegularExpressions;

public class Day3
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;

        //December 3
        var nastyCode = File.ReadAllText($@"{root}\input3.txt");
        var matches = new Regex(@"mul\((?<a>[0-9]+),(?<b>[0-9]+)\)")
            .Matches(nastyCode);

        var multiplied = matches
            .Select(x => int.Parse(x.Groups["a"].Value) * int.Parse(x.Groups["b"].Value))
            .Sum();

        Console.WriteLine($"Sum: {multiplied}"); //170807108

        //December 3, Part 2
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

        Console.WriteLine($"Sum with do and don't: {sum}");
    }
}