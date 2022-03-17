namespace ConsoleDemoApp
{
    using System;

    public static class ArraySort
    {
        public static void SortRows(Func<int[], int> comparisonResult, Func<int, int, bool> orderType, int[][] arrayToSort)
        {
            if (arrayToSort == null)
            {
                throw new ArgumentNullException(nameof(arrayToSort));
            }

            int[] tempArray;
            for (int write = 0; write < arrayToSort.Length; write++)
            {
                for (int sort = 0; sort < arrayToSort.Length - 1; sort++)
                {
                    if (orderType(comparisonResult(arrayToSort[sort]), comparisonResult(arrayToSort[sort + 1])))
                    {
                        tempArray = arrayToSort[sort + 1];
                        arrayToSort[sort + 1] = arrayToSort[sort];
                        arrayToSort[sort] = tempArray;
                    }
                }
            }
        }

        public static bool Desc(int left, int right)
        {
            return left > right;
        }

        public static bool Asc(int left, int right)
        {
            return left < right;
        }

        public static int Sum(int[] rowToSum)
        {
            int rowElementsSum = 0;
            for (int i = 0; i < rowToSum.Length; i++)
            {
                rowElementsSum += rowToSum[i];
            }

            return rowElementsSum;
        }

        public static int MaxValue(int[] rowToCompare)
        {
            int maxValue = int.MinValue;
            for (int i = 0; i < rowToCompare.Length; i++)
            {
                if (maxValue < rowToCompare[i])
                {
                    maxValue = rowToCompare[i];
                }
            }

            return maxValue;
        }

        public static int MinValue(int[] rowToCompare)
        {
            int minValue = int.MaxValue;
            for (int i = 0; i < rowToCompare.Length; i++)
            {
                if (minValue > rowToCompare[i])
                {
                    minValue = rowToCompare[i];
                }
            }

            return minValue;
        }
    }
}