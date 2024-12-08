namespace AdventOfCode;

public class Day6
{
    public void Run()
    {
        var moveOffset = new Dictionary<char, int[]>()
        {
            { '>', [0, 1] },
            { 'v', [1, 0] },
            { '<', [0, -1] },
            { '^', [-1, 0] },
        };
        var moveOrder = new[] { '>', 'v', '<', '^' };
        var (map, pos) = ReadMap();

        //walk map
        while (true)
        {
            var move = moveOffset[pos.Direction];
            var newI = pos.I + move[0];
            var newJ = pos.J + move[1];

            //is out of map
            var inMap = newI >= 0 && newI < map.GetLength(0) && newJ >= 0 && newJ < map.GetLength(1);
            if (!inMap)
            {
                map[pos.I, pos.J] = 'X';
                break;
            }

            //need rotation
            else if (map[newI, newJ] == '#')
            {
                var newDir = NextDirection(pos.Direction);
                pos = new Pos(pos.I, pos.J, newDir);
            }

            //need movement
            else
            {
                map[pos.I, pos.J] = 'X';
                pos = new Pos(newI, newJ, pos.Direction);
            }
        }

        Console.WriteLine($"Num X is {Count()}"); //5444


        char NextDirection(char current)
        {
            var idx = Array.IndexOf(moveOrder, current);
            return moveOrder[(idx + 1) % moveOrder.Length];
        }

        int Count()
        {
            var count = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'X')
                        count++;
                }
            }

            return count;
        }


        Tuple<char[,], Pos> ReadMap()
        {
            var root = Environment.CurrentDirectory;
            var lines = File.ReadAllLines($@"{root}\input6.txt");
            var map = new char[lines.Length, lines[0].Length];
            var position = new Pos(0, 0, 'v');

            //populate map and pos
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[0].Length; j++)
                {
                    var letter = lines[i][j];
                    if (moveOrder.Contains(letter))
                    {
                        position = new Pos(i, j, letter);
                        map[i, j] = 'X';
                    }
                    else
                    {
                        map[i, j] = letter;
                    }
                }
            }

            return new Tuple<char[,], Pos>(map, position);
        }
    }
}

public record Pos(int I, int J, char Direction);
