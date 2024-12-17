namespace AdventOfCode;

public class Day16
{
    public void Run()
    {
        var costRotate = 1000;
        var costMove = 1;

        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input16.txt");
        var map = new char[lines.Length, lines.First().Length];
        var costs = new Dictionary<Reindeer, int>();
        Initialize(map.GetLength(0), map.GetLength(1), int.MaxValue);
        var startReindeer = new Reindeer(0, 0, '>');
        var end = new Pt(-1, -1);
        var offsets = new Dictionary<char, int[]>
        {
            { '>', new[] { 1, 0 } },
            { 'v', new[] { 0, 1 } },
            { '<', new[] { -1, 0 } },
            { '^', new[] { 0, -1 } },
        };
        var offsetKeys = offsets.Keys.ToArray();

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines.First().Length; j++)
            {
                var chr = lines[i][j];
                map[i, j] = chr;
                if (chr == 'S')
                {
                    startReindeer = new Reindeer(i, j, '>');
                }

                if (chr == 'E')
                {
                    end = new Pt(i, j);
                }
            }
        }

        Find(); //85396 

        var c = costs.Where(r => r.Key.I == end.I && r.Key.J == end.J).Min(x=>x.Value);
        Console.WriteLine(c); 
 
        void Find()
        {
            var minCost = int.MaxValue;
            var tasks = new List<ReindeerCost>() { new ReindeerCost(startReindeer,0) };
            
            while(true)
            {
                if (tasks.Count == 0)
                    break;

                var task = tasks.First();
                tasks.RemoveAt(0);
                
                var reindeer = task.Reindeer;
                var cost = task.Cost;
                
                //too expensive
                if (cost >= minCost)
                {
                    continue;
                }
                
                //arrived at stop
                if (map[reindeer.I, reindeer.J] == '#')
                {
                    continue;
                }

                //been here before
                if (cost > costs[reindeer])
                    continue;

                costs[reindeer] = cost;

                //arrived at end
                if (map[reindeer.I, reindeer.J] == 'E')
                {
                    minCost = cost;
                    continue;
                }

                var offset = offsets[reindeer.Direction];

                //Move
                var c  =cost +costMove;
                tasks.Add( new ReindeerCost(new Reindeer(reindeer.I + offset[1], reindeer.J + offset[0], reindeer.Direction), c));
                
                //90 CW
                c = cost + costRotate;
                tasks.Add( new ReindeerCost(new Reindeer(reindeer.I, reindeer.J, Rotate(reindeer.Direction, 1)),c));

                //90 CCW
                c = cost + costRotate;
                tasks.Add(new ReindeerCost(new Reindeer(reindeer.I, reindeer.J, Rotate(reindeer.Direction, -1)), c));
                
                //180
                c = cost + costRotate * 2;
                tasks.Add(new ReindeerCost(new Reindeer(reindeer.I ,reindeer.J, Rotate(reindeer.Direction, 2)),c));
            }
        }

        char Rotate(char chr, int offset)
        {
            var idx = Array.IndexOf(offsetKeys, chr) + offset;
            return offsetKeys[Mod(idx,offsetKeys.Length)];
        }

        void Initialize(int iL, int jL, int v)
        {
            for (int i = 0; i < iL; i++)
            {
                for (int j = 0; j < jL; j++)
                {
                    costs[new Reindeer(i,j,'>')] = v;
                    costs[new Reindeer(i,j,'v')] = v;
                    costs[new Reindeer(i,j,'<')] = v;
                    costs[new Reindeer(i,j,'^')] = v;
                }
            }
        }
    }

    record ReindeerCost(Reindeer Reindeer, int Cost);

    record Reindeer(int I, int J, char Direction);

    record Pt(int I, int J);
    
    static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
