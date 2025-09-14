using JetBrains.Annotations;
using Xunit;

namespace AgileroC.Tests;

[TestSubject(typeof(MatrixCalculator))]
public class MatrixCalculatorTest
{

     [Fact]
        public void MatrixCalculate_3x3Matrix()
        {
            int[,] matrix = {
                {1, 2, 3},
                {2, 4, 5},
                {3, 5, 6}
            };
            
            int[,] expected = {
                {2, 5},
                {5, 2}
            };

            var result = MatrixCalculator.MatrixCalculate(matrix);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MatrixCalculate_5x5Matrix()
        {
            int[,] matrix = {
                {0, 1, 2, 3, 4},
                {1, 1, 2, 3, 4},
                {2, 2, 2, 3, 4},
                {3, 3, 3, 3, 4},
                {4, 4, 4, 4, 4}
            };
            
           int[,] expected = {
                {8, 15},
                {15, 8}
            };

            var result = MatrixCalculator.MatrixCalculate(matrix);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MatrixCalculate_4x4Matrix()
        {
            int[,] matrix =
            {
                { 1, 2, 3, 4 },
                { 2, 5, 6, 7 },
                { 3, 6, 8, 9 },
                { 4, 7, 9, 10 }
            };

            int[,] expected =
            {
                { 5, 16 },
                { 16, 5 }
            };

            var result = MatrixCalculator.MatrixCalculate(matrix);

            Assert.Equal(expected, result);
        }
}