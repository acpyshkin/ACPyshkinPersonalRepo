namespace FirstLibrary
{
    internal class Item<T>
    {
        internal Item<T>? Prev { get; set; }
        internal Item<T>? Next { get; set; }

        internal T? Value { get; set; }

        internal void Clear()
        {
            Prev = null;
            Next = null;
            Value = default;
        }
    }
}