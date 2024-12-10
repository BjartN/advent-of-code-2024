namespace AdventOfCode;

public class Day9Part2
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var line = File.ReadAllLines($@"{root}\input9.txt").First().ToCharArray();

        var l = new List<Fil>();
        for (int i = 0; i < line.Length; i += 2)
        {
            var len = int.Parse(line[i].ToString());
            var free = (i + 1) < line.Length ? int.Parse(line[i + 1].ToString()) : 0;

            l.Add(new Fil(i / 2, len, true)); //disk
            l.Add(new Fil(i / 2, free, false)); //free
        }

        //compact
        var iStart = 0;
        for (int iEnd = l.Count - 1; iEnd >= 0; iEnd--)
        {
            var fileToMove = l[iEnd];
            if (!fileToMove.IsFile)
                continue;

            var freeSpace = l.Select((file,i) =>new{ file,i}).FirstOrDefault(x => x.i<iEnd && !x.file.IsFile && x.file.Value>=fileToMove.Value);
            if(freeSpace==null)
                continue;
            
            l[iEnd] = new Fil(-1, fileToMove.Value, false);
            l[freeSpace.i] = new Fil(-1, freeSpace.file.Value - fileToMove.Value, false);
            l.Insert(freeSpace.i, fileToMove);
            
        }

        var lExpand = new List<int?>();
        foreach (var item in l)
        {
            for (int i = 0; i < item.Value; i++)
            {
                lExpand.Add(item.IsFile ? item.Id : null);
            }
        }

        //compacting final step
        var sum = lExpand.Select((x, i) => x.HasValue ? x.Value * i : 0d).Sum();
        Console.WriteLine(sum); //6287317016845
    }
}

public record Fil(int Id, int Value, bool IsFile);