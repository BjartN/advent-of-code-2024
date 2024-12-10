
namespace AdventOfCode;

public class Day9
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var line = File.ReadAllLines($@"{root}\input9.txt").First().ToCharArray();

        //extract
        var idx = 0;
        var l = new List<int?>();
        for (int i = 0; i < line.Length; i+=2)
        {
            var len = int.Parse(line[i].ToString());
            var free =(i+1)<line.Length ? int.Parse(line[i+1].ToString()) : 0;
            
            for (var j = 0; j < len; j++)
                l.Add(i/2);

            for (var j = 0; j < free; j++)
                l.Add(null);
        }
        Print();
        
        //compact
        var iStart = 0;
        for (int iEnd = l.Count-1; iEnd >=0 ; iEnd--)
        {
            if (l[iEnd] == null && iStart>=iEnd)
                continue;
        
            //move iStart to first "."
            while (iStart < iEnd)
            {
                if (l[iStart] == null)
                {
                    l[iStart] = l[iEnd];
                    l[iEnd] = null;
                    break;
                }
                iStart++;
            }
        }
        
        //compacting final step
        var sum = l.Select((x, i) => x.HasValue ? x.Value * i : 0d).Sum();
        Console.WriteLine(sum); //6262891638328

        void Print()
        {
            foreach(var item in l)
                Console.Write(item==null ?  "." : item.ToString());
            Console.WriteLine();
        }
    }
}