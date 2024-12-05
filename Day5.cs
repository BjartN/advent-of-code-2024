namespace AdventOfCode;

public class Day5
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input5.txt");
        var emptyLineIdx = lines.Select((x, i) => new { x, i }).Where(x => x.x.Trim() == "").Select(x => x.i).First();

        var rules = lines.Take(emptyLineIdx)
            .Select(line => line.Split('|'))
            .Select(pair => new { a = int.Parse(pair[0]), b = int.Parse(pair[1]) })
            .GroupBy(x => x.a).ToDictionary(x => x.Key, x => x.Select(y => y.b).ToArray());

        var updates = lines.Skip(emptyLineIdx + 1)
            .Select(x => x.Split(','))
            .Select(ary => ary.Select(x=>int.Parse(x)!).ToArray())
            .ToArray();

        var ok = new List<int>();
        var corrected = new List<int>();
        
        foreach (var update in updates)
        {
            var correct = update.ToList();
            correct.Sort(Compare);

            if (correct.SequenceEqual(update))
            {
                ok.Add(correct[correct.Count/2]);
            }
            else
            {
                corrected.Add(correct[correct.Count/2]);
            }
        }
        
        Console.WriteLine($"OK {ok.Sum()}"); //5991
        Console.WriteLine($"Corrected {corrected.Sum()}"); //5479

        int Compare(int a, int b)
        {
            var rule1= rules.TryGetValue(a, out int[]? v1) ? v1 : [];
            var rule2= rules.TryGetValue(b, out int[]? v2) ? v2 : [];

            if (rule1.Contains(b))
                return -1; //a<b

            if (rule2.Contains(a))
                return 1; //a>b

            return 0;
        }
    }
}