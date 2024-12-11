namespace AdventOfCode;

public class Day11
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var numbers = File.ReadAllLines($@"{root}\input11.txt").First().Split(' ').ToList();

        for (int iteration = 1; iteration <= 75 ; iteration++)
        {
             //for each number in list
             var l = new List<string>();
             foreach (var n in numbers)
             {
                 if(n=="0"){
                     l.Add("1");
                 }
                 else if (n.Length % 2 == 0)
                 {
                     l.Add(double.Parse(n.Substring(0,n.Length/2)).ToString());
                     l.Add(double.Parse(n.Substring(n.Length/2)).ToString());
                 }
                 else
                 {
                     l.Add((double.Parse(n)*2024).ToString());
                 }
             }
             
             Console.WriteLine($"{iteration}: {numbers.Count}");
             numbers = l;
        }
        
        Console.WriteLine($"Num stones: {numbers.Count}");
    }
}