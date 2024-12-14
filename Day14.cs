namespace AdventOfCode;
using System;
using System.Drawing;

public class Day14
{
    public void Run()
    {
        var d = DateTime.UtcNow;
        var folder = @$"c:/temp/{d.ToString(@"yyyy-MM-dd hh\hmm\mss\s")}";
        Directory.CreateDirectory(folder);
        
        //p=0,4 v=3,-3

        var root = Environment.CurrentDirectory;
        var robots = File.ReadAllLines($@"{root}\input14.txt")
            .Select(line => line.Split(' '))
            .Select(pair => new
            {
                p = pair[0].Substring(2).Split(','),
                v = pair[1].Substring(2).Split(',')
            })
            .Select(x => new Robot(new P(int.Parse(x.p[0]), int.Parse(x.p[1])),
                new V(int.Parse(x.v[0]), int.Parse(x.v[1]))))
            .ToArray();

        var totalSeconds = 10000;
        var width = 101;
        var height = 103;

        Print(0);

        for (var second = 1; second <= totalSeconds; second++)
        {
            for (var i = 0; i < robots.Length; i++)
            {
                var r = robots[i];
                var dx = r.V.X;
                var dy = r.V.Y;

                var p = new P(
                   Mod( (r.P.X + dx) ,width), 
                   Mod( (r.P.Y + dy) , height)
                    );

                robots[i] = new Robot(p, r.V);
            }
            
            if(second>2000)
                PrintImage(second); //print a shit load of images and verify if they look like a tree "manually".. answer is 6512
        }

        //compute safety factor
        var ul = robots
            .Where(r => r.P.X < (width / 2))
            .Where(r => r.P.Y < (height / 2)).ToArray();

        var ur = robots
            .Where(r => r.P.X > (width / 2))
            .Where(r => r.P.Y < (height / 2)).ToArray();

        var ll = robots
            .Where(r => r.P.X < (width / 2))
            .Where(r => r.P.Y > (height / 2)).ToArray();

        var lr = robots
            .Where(r => r.P.X > (width / 2))
            .Where(r => r.P.Y > (height / 2)).ToArray();

        Console.WriteLine();
        Console.WriteLine($"UL: {ul.Length}");
        Console.WriteLine($"UR: {ur.Length}");
        Console.WriteLine($"LL: {ll.Length}");
        Console.WriteLine($"LR: {lr.Length}");

        var sum = ul.Count() * ur.Count() * ll.Count() * lr.Count();

        Console.WriteLine($"Safety factor: {sum}"); //218433348 too low


        void PrintImage(int second)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var isMiddel = x == width / 2 || y+1==height/2+1;
                    if (isMiddel)
                    {
                        bitmap.SetPixel(x, y, Color.White);
                        continue;
                    }
                    
                    var c = robots.Count(r => r.P.Y == y && r.P.X == x);
                    
                    if(c==0)
                        bitmap.SetPixel(x, y, Color.White);
                    else
                        bitmap.SetPixel(x, y, Color.Black);
                }
            }
            bitmap.Save( Path.Combine(folder,$"{second}.png"));
        }
        
        void Print(int second)
        {
            Console.WriteLine();
            Console.WriteLine($"After {second} second(s):");
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var isMiddel = x == width / 2 || y+1==height/2+1;
                    if (isMiddel)
                    {
                        Console.Write(" ");
                        continue;
                    }
                    
                    var c = robots.Count(r => r.P.Y == y && r.P.X == x);
                    Console.Write(c==0 ? "." : $"{c}");
                }
                Console.WriteLine();
            }
        }
    }
    
    
    static int Mod(int x, int m) {
        return (x%m + m)%m;
    }
}


public record Robot(P P, V V);

public record P(int X, int Y);

public record V(int X, int Y);