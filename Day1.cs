using System.Text.RegularExpressions;

public class Day1
{
    public void Run()
    {
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
        var r = left.Select((x, i) => Math.Abs(right[i] - x)).Sum();
        Console.WriteLine($"Diff: {r}");
        
        //December 1, Part 2
        var rightCount = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var similarityScore = left.Select(x => rightCount.TryGetValue(x, out var count) ? count * x : 0).Sum();
        Console.WriteLine($"SimilarityScore: {similarityScore}");
    }
}