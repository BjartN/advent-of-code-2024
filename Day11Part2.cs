namespace AdventOfCode;

public class Day11Part2
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var numbers = File.ReadAllLines($@"{root}\input11.txt").First().Split(' ').ToList();
        
        var memory = new Dictionary<F, long>();
        long total = 0;
        foreach (var n in numbers)
        {
            total+= DevelopNumber(n, 1, 75);
        }
        Console.WriteLine(total); //229557103025807

        long DevelopNumber(string n, int i, int iterations)
        {
            var f = new F(n, i);
            if (memory.TryGetValue(f, out var number))
                return number;
            
            if (i-1 == iterations)
            {
                return 1;
            }
            
            if (n == "0")
            {
                var result = DevelopNumber("1",i+1,iterations);
                memory[f] = result;
                return result;
            }
            else if (n.Length % 2 == 0)
            {
                var result = DevelopNumber(double.Parse(n.Substring(0, n.Length / 2)).ToString(), i+1,iterations)
                 + DevelopNumber(double.Parse(n.Substring(n.Length / 2)).ToString(), i+1,iterations);
                memory[f] = result;
                return result;
            }
            else
            {
                var result = DevelopNumber((double.Parse(n) * 2024).ToString(),i+1,iterations);
                memory[f] = result;
                return result;
            }
        }
    }

}

public record F(string N, int Iterations);