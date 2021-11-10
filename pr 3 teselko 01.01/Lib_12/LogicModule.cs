using System;

namespace Lib_12
{
    public class LogicModule
    {
        public (int[] maxValues, int min) MinAmongMax (int [,] matrix)
        {
            int[] maxValues = new int[matrix.GetLength(1)];
            int max, min;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                max = int.MinValue;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[j, i] > max)
                    {
                        max = matrix[j,i];
                    }
                }
                maxValues[i] += max;
            }
            min = maxValues[0];
            for (int i = 0; i < maxValues.Length; i++)
            {
                if (maxValues[i] < min)
                    min = maxValues[i];
            }
            return (maxValues, min);
        }
    }
}
