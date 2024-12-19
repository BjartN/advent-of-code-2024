namespace AdventOfCode;

public class Day19
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input19.txt");
        var towles = lines[0].Split(',').Select(x => x.Trim()).ToArray();
        var patterns = lines.Skip(2).Select(x => x.Trim()).ToArray();
        var total = 0;


        for (var i = 0; i < patterns.Length; i++)
        {
            var p = patterns[i];
            if (Match(p))
            {
                total++;
                Console.WriteLine(p);                
            }
        }

        Console.WriteLine("Total is " + total); //350
        

        bool Match(string pattern)
        {
            if (pattern.Length == 0)
            {
                return true;
            }
            
            //find all patterns intersecing the middle of the string
            var middle = pattern.Length / 2;
            var relevantTowles = towles.Where(pattern.Contains).ToArray();

            foreach (var towel in relevantTowles)
            {
                for (var i = Math.Max(0, middle - towel.Length); i <= middle; i++)
                {
                    if (pattern.Substring(i).StartsWith(towel))
                    {
                        var before = pattern.Substring(0, i);
                        var after = pattern.Substring(i + towel.Length);

                        var success= Match(before) && Match(after);
                        if (success)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}