using System.Text;

namespace AdventOfCode;

public class Day15
{
    // ########
    // #..O.O.#
    // ##@.O..#
    // #...O..#
    // #.#.O..#
    // #...O..#
    // #......#
    // ########
    //
    //  <^^>>>vv<v>>v<<


    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input15.txt");
        var mapLines = lines.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).ToArray();

        var offsets = new Dictionary<char, int[]>
        {
            { '>', new[] { 1, 0 } },
            { '<', new[] { -1, 0 } },
            { 'v', new[] { 0, 1 } },
            { '^', new[] { 0, -1 } },
        };

        Pt robot = new Pt(0, 0);
        var map = new char[mapLines.Count(), mapLines.First().Length];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                var chr = mapLines[i][j];
                map[i, j] = chr;
                if (chr == '@')
                    robot = new Pt(i, j);
            }
        }

        var moves = lines
            .Skip(map.GetLength(0) + 1).Aggregate(new StringBuilder(), (sb, line) => sb.Append(line.Trim()))
            .ToString();

        //Console.WriteLine(moves);
        //Print();

        foreach (var move in moves)
        {
            //Console.WriteLine();
            //Console.WriteLine($"Move {move}: ");
            
            Move(move);
            //Print();
        }
        Console.WriteLine($"Sum is {Sum()}");
        
        void Move(char direction)
        {
            var offset = offsets[direction];
            var pi = robot.I;
            var pj = robot.J;

            //find free space
            while (true)
            {
                pi += offset[1];
                pj += offset[0];

                var chr = map[pi, pj];

                if (chr == '.')
                {
                    //free space roll back
                    while (true)
                    {
                        var prevI = pi - offset[1];
                        var prevJ = pj - offset[0];
                        
                        map[pi, pj] = map[prevI,prevJ]; //move object
                        map[prevI, prevJ] = '.';        //leave .

                        if (map[pi, pj] == '@')
                        {
                            robot = new Pt(pi, pj);
                            break; //stop once back to @ sign
                        }

                        pi = prevI;
                        pj = prevJ;
                    }

                    break;
                }
                else if (chr == 'O')
                {
                    //continue search
                }
                else if (chr == '#')
                {
                    break; //hit a wall.. stop
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
        
        

        void Print()
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }
        
        int Sum()
        {
            var sum = 0;
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'O')
                    {
                        sum = sum + (i * 100 + j); //"GPS"
                    }
                }
            }
            return sum;
        }

    }

    record Pt(int I, int J);
}