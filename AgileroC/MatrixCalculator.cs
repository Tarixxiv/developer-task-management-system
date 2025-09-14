namespace AgileroC;

public class MatrixCalculator
{
    public static int[,] MatrixCalculate(int[,] matrix)
    {
        var sumBottom = 0;
        var sumLeft = 0;

        for (var row = 1; row < matrix.GetLength(0); row++)
        {
            //Column index of right diagonal for current column determines if current cell is in left or bottom triangle
            var columnOfRightDiagonal = GetColumnOfRightDiagonal(matrix, row);
            //We want to only the left and bottom triangles, rest is symmetrical
            for (var column = 0; column < row; column++)
            {
                if (column < columnOfRightDiagonal)
                {
                    sumLeft += matrix[row, column];
                }
                else if (column > columnOfRightDiagonal)
                {
                    sumBottom += matrix[row, column];
                }
            }
        }

        int[,] result =
        {
            { sumLeft, sumBottom },
            { sumBottom, sumLeft }
        };

        return result;
    }

    //Gets column index of right matrix diagonal for given row 
    private static int GetColumnOfRightDiagonal(int[,] matrix, int row)
    {
        return matrix.GetLength(0) - row - 1;
    }
}