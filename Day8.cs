namespace AdventOfCode;

public class Day8
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input8.txt");
        var antennas = new List<Tuple<int, int, char>>();
        var result = new HashSet<Tuple<int, int>>();
        
        var map = new char[lines.Length, lines[0].Length];
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[0].Length; j++)
            {
                map[i, j] = lines[i][j];
                if(map[i,j]!='.')
                   antennas.Add(new Tuple<int, int, char>(i,j,map[i,j]));
            }
        }

        Compute1();
        Console.WriteLine(result.Count); //348
        
        result = new HashSet<Tuple<int, int>>();
        Compute2();

        Console.WriteLine(result.Count); //1221

        void Compute2()
        {
            for (int i = 0; i < antennas.Count; i++)
            {
                var antenna = antennas[i];
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var nextAntenna = antennas[j];
                    if(nextAntenna.Item3 != antenna.Item3)
                        continue;

                    //fill out the line
                    var di= nextAntenna.Item1 - antenna.Item1;
                    var dj = nextAntenna.Item2 - antenna.Item2;

                    //going up
                    var x = antenna.Item1;
                    var y = antenna.Item2;
                    do
                    {
                        result.Add(new Tuple<int, int>(x, y));
                        x -= di;
                        y -= dj;
                    } while (x>=0  && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1));

                    //going down
                    x = antenna.Item1;
                    y = antenna.Item2;
                    do
                    {
                        result.Add(new Tuple<int, int>(x, y));
                        x += di;
                        y += dj;
                    } while (x>=0  && x < map.GetLength(0)  && y >= 0 && y < map.GetLength(1));

                }
                
            }
        }
        
        void Compute1()
        {
             
            for (var i = 0; i < antennas.Count; i++)
            {
                var antenna = antennas[i];
                for (var j = i+1; j < antennas.Count; j++)
                {
                    var nextAntenna = antennas[j]; 
                    if(nextAntenna.Item3 != antenna.Item3)
                        continue;

                    var di= nextAntenna.Item1 - antenna.Item1;
                    var dj = nextAntenna.Item2 - antenna.Item2;

                    var candidateI = nextAntenna.Item1 + di;
                    var candidateJ = nextAntenna.Item2 + dj;
                    if(candidateI>=0 && candidateI<map.GetLength(0) && candidateJ>=0 && candidateJ<map.GetLength(1))
                        result.Add(new Tuple<int, int>(candidateI, candidateJ));
                
                    var candidateI2 = antenna.Item1 - di;
                    var candidateJ2 = antenna.Item2 - dj;
                    if(candidateI2>=0 && candidateI2<map.GetLength(0) && candidateJ2>=0 && candidateJ2<map.GetLength(1))
                        result.Add(new Tuple<int, int>(candidateI2, candidateJ2));
                }
            }
        }
        
        void DrawMap()
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if(result.Contains(new Tuple<int, int>(i,j)))
                        Console.Write('#');
                    else
                        Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }
    }
}