using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode;

public class Day12Part2
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input12.txt");
        var map = new char[lines.Length, lines.First().Length];
        var visited = new bool[lines.Length, lines.First().Length];
        var directions = new int[][]
        {
            [-1, 0],    //above
            [1, 0],     //below
            [0, 1],     //right
            [0, -1],    //left
        };

        //make all the perimeters point in a clockwise direction, so that you can connect the end of a perimeter with the start of the next when 
        //iterating the cycle
        var perimeterFunctions = new Func<int, int, Perimeter>[]
        {
            //above (go left right)
            (i,j) =>new Perimeter(new Point(i,j), new Point(i,j+1)),
            //below (go right left)
            (i,j) =>new Perimeter(new Point(i+1,j+1),new Point(i+1,j)),
            //right (go top down)
            (i,j) =>new Perimeter(new Point(i,j+1),new Point(i+1,j+1)),
            //left (go down top)
            (i,j) => new Perimeter(new Point(i+1,j),new Point(i,j))
        };
        

        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = lines[i][j];
            }
        }

        var sides = 0;
        var total = 0;
        var area = 0;
        List<Perimeter> perimeters;
        
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if(visited[i,j])
                    continue;

                perimeters = new List<Perimeter>();
                sides = 0;
                area = 0;
                BuildRegion(i,j);
                CountSides(perimeters.First());
                
                total += area * sides;
            }
        }
        Console.WriteLine(total); //830516

        void CountSides(Perimeter p)
        {
            perimeters.Remove(p);
            if (perimeters.Count == 0)
            {
                sides++;
                return;
            }

            //Walk clockwise
            var next = perimeters.FirstOrDefault(x => x.Start.I == p.End.I && x.Start.J == p.End.J);
            if (next == null)
            {
                //we have completed a cycle, but there are more sides left due to a "hole" in the polygon
                sides++;
                CountSides(perimeters.First());
                return;
            }
            
            //does it change direction
            var di = p.Start.I - p.End.I;
            var dj = p.Start.J - p.End.J;
            var dx = next.Start.I - next.End.I;
            var dy = next.Start.J - next.End.J;

            if (di == dx && dj == dy)
            {
                //same direction
                CountSides(next);
            }
            else
            {
                sides++;
                CountSides(next);
            }

        }
        
        void BuildRegion(int i, int j)
        {
            var chr = map[i, j];
            visited[i, j] = true;
            area++;

            for (int k = 0; k < directions.Length; k++)
            {
                var direction = directions[k];
                var f = perimeterFunctions[k];
           
                var x = i + direction[0];
                var y = j + direction[1];

                if (IsPerimeter(x, y, chr))
                {
                    perimeters.Add(f(i,j));
                }
                else if(!visited[x,y]){
                    BuildRegion(x,y);
                }
            }
        }

        bool IsPerimeter(int i,int j,char chr) => !InMap(i, j) || map[i, j] != chr;
        bool InMap(int i,int j) =>i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1);
    }
}

public record Point(int I, int J);
public record Perimeter(Point Start, Point End);