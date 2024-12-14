namespace AdventOfCode;

public class Day13Part2
{
    public void Run()
    {
        throw new NotImplementedException("This part is not implemented");
        
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input13.txt");
        var machines = new List<Machine>();
        var costs = new Dictionary<Press,long>();
        
        for (int i = 0; i < lines.Length; i += 4)
        {
            var prize = lines[i + 2];
            var a = ParseButton(lines[i]);
            var b = ParseButton(lines[i + 1]);
            var p = ParsePrize(prize);

            machines.Add(new Machine(a, b, p));
        }

        Console.WriteLine($"Num machines: {machines.Count}");

        long total = 0;
        foreach (var machine in machines)
        {
            costs = new Dictionary<Press, long>();
            var x = FindCost(0,0, machine);

            if (x == int.MaxValue)
            {
                Console.WriteLine("No solution");
            }
            else
            {
                total += x;
                Console.WriteLine($"Machine:{x}");
            }
        }
        Console.WriteLine($"Total: {total}"); //27105
        
        
        long FindCost(long aPress, long bPress, Machine m)
        {
            var p = new Press(aPress, bPress);
            if (costs.TryGetValue(p, out var pCost))
                return pCost;
            
            var x = aPress * m.A.XOffset + bPress * m.B.XOffset;
            var y = aPress * m.A.YOffset + bPress * m.B.YOffset;
            
            if (aPress == 100 || bPress == 100 || x > m.Prize.X || y > m.Prize.Y)
            {
                costs[p] = long.MaxValue;
                return long.MaxValue;
            }
            else if (m.Prize.X == x && m.Prize.Y == y)
            {
                //it costs 3 tokens to push the A button and 1 token to push the B button
                var cost = aPress * 3 + bPress * 1;
                costs[p] = cost;
                return cost;
            }
            else
            {
                var cost1 = FindCost(aPress + 1, bPress, m);
                var cost2 = FindCost( aPress, bPress+1, m);
                var cost=  Math.Min(cost1, cost2);
                costs[p] = cost;
                return cost;
            }
        }
    }



    Prize ParsePrize(string prize)
    {
        var p = prize.Split(':').Last().Split(',').Select(x => x.Trim()).Select(x => int.Parse(x.Substring(2)))
            .ToArray();
        return new Prize(p[0]+10000000000000, p[1]+10000000000000);
    }

    Button ParseButton(string line)
    {
        var b = line.Split(':').Last().Split(',').Select(x => x.Trim()).Select(x => int.Parse(x.Substring(2)))
            .ToArray();
        return new Button(b[0], b[1]);
    }
}
