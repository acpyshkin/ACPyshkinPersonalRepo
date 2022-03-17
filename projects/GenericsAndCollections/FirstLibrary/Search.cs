namespace FirstLibrary
{
    public static class Search
    {
        public static int BinarySearch<T>(T[] elements, T elementToSearch)
            where T : IComparable<T>
        {
            if (elements.Length == 0)
            {
                throw new ArgumentException("Array is empty");
            }

            // we assume that input array is sorted
            int high = elements.Length - 1;
            int low = 0;

            if (elements[low].CompareTo(elementToSearch) == 0)
            {
                return 0;
            }
            else if (elements[high].CompareTo(elementToSearch) == 0)
            {
                return high;
            }

            while (low <= high)
            {
                int mid = (low + high) / 2;

                int comparison = elements[mid].CompareTo(elementToSearch);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return -1;
        }
    }
}