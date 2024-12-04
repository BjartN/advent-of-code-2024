namespace AdventOfCode;

public class Day4
{
    public void Run()
    {
        var root = Environment.CurrentDirectory;

        //December 4
        var lines = File.ReadAllLines($@"{root}\input4.txt");
        var matrix = new char[lines.Length, lines.First().Length];

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines.First().Length; col++)
            {
                matrix[row, col] = lines[row][col];
            }
        }

        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);


        //left right
        var count = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols - 3; col++)
            {
                var ok = matrix[row, col] == 'X' && matrix[row, col + 1] == 'M' && matrix[row, col + 2] == 'A' &&
                         matrix[row, col + 3] == 'S';
                if (ok)
                    count++;

                var ok2 = matrix[row, col] == 'S' && matrix[row, col + 1] == 'A' && matrix[row, col + 2] == 'M' &&
                          matrix[row, col + 3] == 'X';
                if (ok2)
                    count++;
            }
        }

        //top down
        for (int row = 0; row < rows - 3; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var ok = matrix[row, col] == 'X' && matrix[row + 1, col] == 'M' && matrix[row + 2, col] == 'A' &&
                         matrix[row + 3, col] == 'S';
                if (ok)
                    count++;

                var ok2 = matrix[row, col] == 'S' && matrix[row + 1, col] == 'A' && matrix[row + 2, col] == 'M' &&
                          matrix[row + 3, col] == 'X';
                if (ok2)
                    count++;
            }
        }

        //a diagonal starts at every matrix cell
        for (var row = 0; row < rows - 3; row++)
        {
            for (int col = 0; col < cols - 3; col++)
            {
                var ok = matrix[row, col] == 'X' && matrix[row + 1, col + 1] == 'M' &&
                         matrix[row + 2, col + 2] == 'A' && matrix[row + 3, col + 3] == 'S';
                if (ok)
                    count++;

                var ok2 = matrix[row, col] == 'S' && matrix[row + 1, col + 1] == 'A' &&
                          matrix[row + 2, col + 2] == 'M' && matrix[row + 3, col + 3] == 'X';
                if (ok2)
                    count++;
            }
        }

        for (var row = 3; row < rows; row++)
        {
            for (int col = 0; col < cols - 3; col++)
            {
                var ok = matrix[row, col] == 'X' && matrix[row - 1, col + 1] == 'M' &&
                         matrix[row - 2, col + 2] == 'A' && matrix[row - 3, col + 3] == 'S';
                if (ok)
                    count++;

                var ok2 = matrix[row, col] == 'S' && matrix[row - 1, col + 1] == 'A' &&
                          matrix[row - 2, col + 2] == 'M' && matrix[row - 3, col + 3] == 'X';
                if (ok2)
                    count++;
            }
        }

        Console.WriteLine($"Count is {count}"); //2534


        //December 4 Part 2
        count = 0;
        for (var row = 0; row < rows - 2; row++)
        {
            for (int col = 0; col < cols - 2; col++)
            {
                var ok1 = matrix[row, col] == 'M' && matrix[row + 1, col + 1] == 'A' && matrix[row + 2, col + 2] == 'S';
                var ok2 = matrix[row, col] == 'S' && matrix[row + 1, col + 1] == 'A' && matrix[row + 2, col + 2] == 'M';

                var ok3 = matrix[row + 2, col] == 'M' && matrix[row + 1, col + 1] == 'A' && matrix[row, col + 2] == 'S';
                var ok4 = matrix[row + 2, col] == 'S' && matrix[row + 1, col + 1] == 'A' && matrix[row, col + 2] == 'M';

                if ((ok1 || ok2) && (ok3 || ok4))
                    count++;
            }
        }

        Console.WriteLine($"X OF MAS Count is {count}"); //1866
    }
}