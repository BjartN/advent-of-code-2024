using System.IO.Enumeration;

namespace AdventOfCode;

public class Day10
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input10.txt");
        var map = new int[lines.Length, lines[0].Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                map[i, j] =int.Parse(lines[i][j].ToString());
            }
        }

        var count = 0;
        var pathCount = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] != 0)
                    continue; //not a trailhead

                var s = new HashSet<Tuple<int, int>>();
                var l = new List<Tuple<int, int>>();
                Walk(i,j,s,l);
                count += s.Count;
                pathCount += l.Count;
            }
        }
        Console.WriteLine(count); //698
        Console.WriteLine(pathCount); //1436

        void Walk(int x, int y, HashSet<Tuple<int,int>> endPoints, List<Tuple<int,int>> paths)
        {
            var currentvalue = map[x, y];
            if (currentvalue == 9)
            {
                endPoints.Add(new Tuple<int, int>(x, y));
                paths.Add(new Tuple<int, int>(x, y));
                return;
            }
            
            if (x - 1 >=  0 && map[x - 1, y] == currentvalue+1)
            {
                Walk(x-1,y,endPoints,paths);   
            }
            
            if (x + 1 < map.GetLength(0) && map[x + 1, y] == currentvalue+1)
            {
                Walk(x+1,y,endPoints,paths);   
            }
            
            if (y - 1 >= 0 && map[x, y-1] == currentvalue+1)
            {
                Walk(x,y-1,endPoints,paths);   
            }
            
            if (y + 1 < map.GetLength(1) && map[x, y+1]  == currentvalue+1)
            {
                Walk(x,y+1,endPoints,paths);   
            }
        }
    }
}