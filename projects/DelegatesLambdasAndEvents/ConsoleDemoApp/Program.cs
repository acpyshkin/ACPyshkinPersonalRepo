namespace ConsoleDemoApp
{
    using System;
    using System.Text;
    public class Program
    {
        public static void Main()
        {
            int[][] Matrix = new int[5][];
            Matrix[0] = new int[] { 15, 0, 100, -791, 6 };
            Matrix[1] = new int[] { -54985, 910, 392, 1940, -17 };
            Matrix[2] = new int[] { 0, 9, 384, -809, 9160 };
            Matrix[3] = new int[] { 8, 0, 57, 72, -45 };
            Matrix[4] = new int[] { 669, 0, 0, 0, 22 };
            ShowArray(Matrix);

            ArraySort.SortRows(ArraySort.Sum, ArraySort.Desc, Matrix);
            ShowArray(Matrix);

            ArraySort.SortRows(ArraySort.MaxValue, ArraySort.Asc, Matrix);
            ShowArray(Matrix);

            ArraySort.SortRows(ArraySort.MinValue, ArraySort.Asc, Matrix);
            ShowArray(Matrix);

            Countdown countdown = new Countdown();
            countdown.Subscrubing += Countdown_Subscrubing;
            countdown.Subscrubing += Countdown_Subscrubing2;

            countdown.Publish(3);
            Thread.Sleep(5000);
            countdown.Subscrubing += Countdown_Subscrubing3;
            countdown.Subscrubing += Countdown_Subscrubing4;
        }

        private static void Countdown_Subscrubing(object? sender, SubscribingEventArgs e)
        {
            if (e.Message != null)
            {
                Console.WriteLine("Method 1" + e.Message);
            }
        }

        private static void Countdown_Subscrubing2(object? sender, SubscribingEventArgs e)
        {
            if (e.Message != null)
            {
                Console.WriteLine("Method 2" + e.Message);
            }
        }

        private static void Countdown_Subscrubing3(object? sender, SubscribingEventArgs e)
        {
            if (e.Message != null)
            {
                Console.WriteLine("Method 3" + e.Message);
            }
        }

        private static void Countdown_Subscrubing4(object? sender, SubscribingEventArgs e)
        {
            if (e.Message != null)
            {
                Console.WriteLine("Method 4" + e.Message);
            }
        }

        public static void ShowArray(int[][] arrayToShow)
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < arrayToShow.Length; row++)
            {
                for (int column = 0; column < arrayToShow[row].Length; column++)
                {
                    sb.Append(arrayToShow[row][column] + " ");
                }

                sb.Append('\n');
            }

            Console.WriteLine(sb.ToString());
        }
    }
}