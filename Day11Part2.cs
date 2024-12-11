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
            total += DevelopNumber(n, 1, 75);
        }
        Console.WriteLine(total); //229557103025807

        long DevelopNumber(string n, int i, int iterations)
        {
            var f = new F(n, i);
            if (memory.TryGetValue(f, out var number))
                return number;

            if (i - 1 == iterations)
            {
                return 1;
            }

            if (n == "0")
            {
                //0 becomes 1
                memory[f] = DevelopNumber("1", i + 1, iterations);
                return memory[f];
            }
            else if (n.Length % 2 == 0)
            {
                //split number
                var start = long.Parse(n.Substring(0, n.Length / 2));
                var end = long.Parse(n.Substring(n.Length / 2));

                memory[f] = DevelopNumber($"{start}", i + 1, iterations) + DevelopNumber($"{end}", i + 1, iterations);
                return memory[f];
            }
            else
            {
                //multiply by 2024
                memory[f] = DevelopNumber($"{long.Parse(n) * 2024}", i + 1, iterations);
                return memory[f];
            }
        }
    }
}

public record F(string N, int Iterations);