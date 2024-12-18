namespace AdventOfCode;

public class Day18
{
    public void Run()
    {
        var gridSize = 71;
        var numBytes = 1024;
        
        var root = Environment.CurrentDirectory;
        var points = File
            .ReadAllLines($@"{root}\input18.txt")
            .Select(line => line.Split(',').Select(int.Parse).ToArray())
            .Take(numBytes)
            .ToArray();

        var offsets = new[]
        {
            new[] { 1, 0 } ,
            new[] { 0, 1 } ,
            new[] { -1, 0 } ,
            new[] { 0, -1 } ,
        };
        
        var map = new char[gridSize, gridSize];
        var costs = new int[gridSize, gridSize];
        
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = '.';
                costs[i, j] = int.MaxValue;
            }
        }

        foreach (var pt in points)
        {
            map[pt[1], pt[0]] = '#';
        }

        Search();
        Console.WriteLine(costs[gridSize-1,gridSize-1]); //282
        
        void Search()
        {
            var minCost = int.MaxValue;
            var tasks = new Queue<Pt>();
            tasks.Enqueue(new Pt(0, 0, 0));

            while (true)
            {
                if (tasks.Count == 0)
                    break;
                var task = tasks.Dequeue();
                
                if(task.X<0 || task.X>gridSize-1)
                    continue;

                if(task.Y<0 || task.Y>gridSize-1)
                    continue;
                
                if (minCost <= task.Cost)
                    continue;
    
                if (map[task.Y, task.X] == '#')
                    continue;

                if(costs[task.Y, task.X]<=task.Cost)
                    continue;

                if (task.X == gridSize - 1 && task.Y == gridSize - 1)
                    minCost = costs[task.Y, task.X];

                costs[task.Y, task.X] = task.Cost;
                
                foreach (var offset in offsets)
                    tasks.Enqueue(new Pt(task.X+offset[1] , task.Y+ offset[0], task.Cost+1));
            }
        }
        
        void Print()
        {
            Console.WriteLine();
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }
    }

    record Pt(int X, int Y,int Cost);
}