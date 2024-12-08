namespace AdventOfCode;

public class Day7
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;
        var lines = File.ReadAllLines($@"{root}\input7.txt")
            .Select(x => x.Split(":"))
            .Select(x => new { value = long.Parse(x[0]), numbers = x[1].Split(' ').Where(y=>long.TryParse(y,out _)).Select(long.Parse).ToArray() })
            .ToArray();

        var total = lines.Where(line => Compute(line.numbers, 1, line.numbers[0], line.value)).Sum(x => x.value);
        var total2 = lines.Where(line => ComputePart2(line.numbers, 1, line.numbers[0], line.value)).Sum(x => x.value);
        
        Console.WriteLine(total); //1399219271639
        Console.WriteLine(total2); //275791737999003

        bool Compute(long[] input, int i, long sum, long target)
        {
            if (sum > target)
                return false;
            
            var s1 = sum * input[i];
            var s2 = sum + input[i];
            
            if (i == input.Length-1) 
            {
                return (s1 == target || s2 == target);
            }
            
            return Compute(input,i+1, s1, target) || Compute(input,i+1, s2, target);
        }
        
        bool ComputePart2(long[] input, int i, long sum, long target)
        {
            if (sum > target)
                return false;

            var s1 = sum * input[i];
            var s2 = sum + input[i];
            var s3 = long.Parse($"{sum}{input[i]}");
            
            if (i == input.Length-1) 
            {
                return (s1 == target || s2 == target || s3==target);
            }
            
            return ComputePart2(input,i+1, s1, target) || ComputePart2(input,i+1, s2, target) || ComputePart2(input,i+1,s3,target);
        }
    }
}