namespace AdventOfCode;

public class Day12
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input12.txt");
        var map = new char[lines.Length, lines.First().Length];
        var visited = new bool[lines.Length, lines.First().Length];
        var directions = new int[][]
        {
            [-1, 0],
            [1, 0],
            [0, 1],
            [0, -1],
        };

        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = lines[i][j];
            }
        }

        var total = 0;
        var perimeter = 0;
        var area = 0;
        
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if(visited[i,j])
                    continue;

                perimeter = 0;
                area = 0;
                BuildRegion(i,j);
                total += area * perimeter;
            }
        }
        Console.WriteLine(total); //1361494

        void BuildRegion(int i, int j)
        {
            var chr = map[i, j];
            visited[i, j] = true;
            area++;

            foreach (var direction in directions)
            {
                var x = i + direction[0];
                var y = j + direction[1]; 
                
                if (IsPerimeter(x,y,chr))
                    perimeter++;
                else if(!visited[x,y])
                    BuildRegion(x,y);
            }
        }
        
        bool IsPerimeter(int i,int j,char chr) => !InMap(i, j) || map[i, j] != chr;
        bool InMap(int i,int j) =>i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1);
    }
}