namespace FirstLibrary
{
    using System.Collections.Generic;
    public static class Fibonacci
    {
        public static IEnumerable<int> Generate()
        {
            int first = 0;
            int second = 1;
            while (true)
            {
                var firstTemp = first;
                first = second;
                second = firstTemp + second;
                yield return second - first;
            }
        }
    }
}