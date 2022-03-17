namespace ConsoleDemoApp.Test
{
    using ConsoleDemoApp;
    using NUnit.Framework;

    [TestFixture]

    internal class ArraySortTest
    {
        [TestCaseSource(nameof(DataForSortRows))]
        public void SortRows_JaggedArrayIsCorrect_ArraySorted(int c, int[][] jaggedArrayToSort, int[][] expectedJaggedArray)
        {
            // arrange , act
            switch (c)
            {
                case 1:
                    ArraySort.SortRows(ArraySort.Sum, ArraySort.Desc, jaggedArrayToSort);
                    break;
                case 2:
                    ArraySort.SortRows(ArraySort.MinValue, ArraySort.Asc, jaggedArrayToSort);
                    break;
                case 3:
                    ArraySort.SortRows(ArraySort.MaxValue, ArraySort.Asc, jaggedArrayToSort);
                    break;
            }

            // assert
            Assert.AreEqual(jaggedArrayToSort, expectedJaggedArray);
        }

        private static readonly int[][] JaggedArrayToSort1 =
        {
            new int[] { 15, 0, 100, -791, 6 },
            new int[] { -54985, 910, 392, 1940, -17 },
            new int[] { 0, 9, 384, -809, 9160 },
            new int[] { 8, 0, 57, 72, -45 },
            new int[] { 669, 0, 0, 0, 22 }
        };
        private static readonly int[][] ExpectedJaggedArraySumDesc =
        {
            new int[] { -54985, 910, 392, 1940, -17 },
            new int[] { 15, 0, 100, -791, 6 },
            new int[] { 8, 0, 57, 72, -45 },
            new int[] { 669, 0, 0, 0, 22 },
            new int[] { 0, 9, 384, -809, 9160 }
        };
        private static readonly int[][] ExpectedJaggedArrayMinAsc =
        {
            new int[] { 669, 0, 0, 0, 22 },
            new int[] { 8, 0, 57, 72, -45 },
            new int[] { 15, 0, 100, -791, 6 },
            new int[] { 0, 9, 384, -809, 9160 },
            new int[] { -54985, 910, 392, 1940, -17 }
        };
        private static readonly int[][] ExpectedJaggedArrayMaxAsc =
        {
            new int[] { 0, 9, 384, -809, 9160 },
            new int[] { -54985, 910, 392, 1940, -17 },
            new int[] { 669, 0, 0, 0, 22 },
            new int[] { 15, 0, 100, -791, 6 },
            new int[] { 8, 0, 57, 72, -45 }
        };

        private static readonly List<TestCaseData> DataForSortRows = new List<TestCaseData>
        {
            new TestCaseData(1, JaggedArrayToSort1, ExpectedJaggedArraySumDesc),
            new TestCaseData(2, JaggedArrayToSort1, ExpectedJaggedArrayMinAsc),
            new TestCaseData(3, JaggedArrayToSort1, ExpectedJaggedArrayMaxAsc)
        };
    }
}