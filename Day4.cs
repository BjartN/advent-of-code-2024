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

        var count = 0;
        for (var row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //left right
                var ok1 =  (col + 3 < cols) && matrix[row, col] == 'X' && matrix[row, col + 1] == 'M' && matrix[row, col + 2] == 'A' && matrix[row, col + 3] == 'S';
                var ok2 =  (col + 3 < cols) && matrix[row, col] == 'S' && matrix[row, col + 1] == 'A' && matrix[row, col + 2] == 'M' && matrix[row, col + 3] == 'X';
                
                //top down
                var ok3 = (row + 3 < rows) && matrix[row, col] == 'X' && matrix[row + 1, col] == 'M' && matrix[row + 2, col] == 'A' && matrix[row + 3, col] == 'S';
                var ok4 = (row + 3 < rows) && matrix[row, col] == 'S' && matrix[row + 1, col] == 'A' && matrix[row + 2, col] == 'M' && matrix[row + 3, col] == 'X';
            
                //diag 1
                var ok5 =  (col + 3 < cols) && (row + 3 < rows) && matrix[row, col] == 'X' && matrix[row + 1, col + 1] == 'M' &&  matrix[row + 2, col + 2] == 'A' && matrix[row + 3, col + 3] == 'S';
                var ok6 =  (col + 3 < cols) && (row + 3 < rows) && matrix[row, col] == 'S' && matrix[row + 1, col + 1] == 'A' &&  matrix[row + 2, col + 2] == 'M' && matrix[row + 3, col + 3] == 'X';
               
                //diag 2
                var ok7 =  (col + 3 < cols) && (row  - 3 >= 0) && matrix[row, col] == 'X' && matrix[row - 1, col + 1] == 'M' && matrix[row - 2, col + 2] == 'A' && matrix[row - 3, col + 3] == 'S';
                var ok8 =  (col + 3 < cols) && (row  - 3 >= 0) && matrix[row, col] == 'S' && matrix[row - 1, col + 1] == 'A' && matrix[row - 2, col + 2] == 'M' && matrix[row - 3, col + 3] == 'X';
              
                if (ok1) count++;
                if (ok2) count++;
                if (ok3) count++;
                if (ok4) count++;
                if (ok5) count++;
                if (ok6) count++;
                if (ok7) count++;
                if (ok8) count++;
            }
        }
        Console.WriteLine($"XMAS count is {count}"); //2476

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