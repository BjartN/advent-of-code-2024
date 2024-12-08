namespace AdventOfCode;

public class Day6Part2
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
        var (initialMap, initialPos) = ReadMap();
        var loopCount = 0;
        
        //Put an obstacle in every possible position in the array, and see if it forms a loop when doing the traversal.
        //A loop is detected if you get to the same place twice moving in the same direction.
        //Brute force as can be :-)
        for (var x = 0; x < initialMap.GetLength(0); x++)
        {
            for (var y = 0; y < initialMap.GetLength(1); y++)
            {
                if (x == initialPos.I && y == initialPos.J)
                    continue;
                
                if (initialMap[x,y] != '.')
                    continue;

                var map = (char[,]) initialMap.Clone();
                //for every cell, keep the history of which direction you were moving when visiting the cell
                var history = CreateHistory(); 
                map[x, y] = '#';

                var pos = new Pos(initialPos.I, initialPos.J, initialPos.Direction);
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

                    //detect loop
                    if (history[pos.I, pos.J].Contains(pos.Direction)){
                        loopCount++;
                        break;
                    }

                    //log history
                    history[pos.I, pos.J].Add(pos.Direction);
                }
            }
        }

        Console.WriteLine($"Loop count is {loopCount}"); //1946

        char NextDirection(char current)
        {
            var idx = Array.IndexOf(moveOrder, current);
            return moveOrder[(idx + 1) % moveOrder.Length];
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

        HashSet<int>[,] CreateHistory()
        {
            var history = new HashSet<int>[initialMap.GetLength(0), initialMap.GetLength(1)];
            for (int i = 0; i < initialMap.GetLength(0); i++)
            {
                for (int j = 0; j < initialMap.GetLength(1); j++)
                {
                    history[i, j] = new HashSet<int>();
                }
            }

            return history;
        }
    }
}