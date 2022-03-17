namespace FirstLibrary
{
    using System;
    using System.Collections.Generic;

    public class Queue<T> : Container<T>
    {
        public Queue() : base() { }
        public Queue(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        public T Dequeue()
        {
            var currentFirst = Begin.Next;

            if (currentFirst == null || currentFirst.Next == null || currentFirst.Value == null)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var value = currentFirst.Value;

            var newFirst = currentFirst.Next;

            Begin.Next = newFirst;
            newFirst.Prev = Begin;

            Count--;

            return value;
        }

        public void Enqueue(T value)
        {
            var currentLast = End.Prev;
            if (currentLast == null)
            {
                throw new ArgumentException("argument is null");
            }

            var item = new Item<T>()
            {
                Prev = currentLast,
                Next = End,
                Value = value
            };

            currentLast.Next = item;
            End.Prev = item;
            Count++;
        }

        public T Peek()
        {
            if (Count == 0 || Begin.Next == null || Begin.Next.Value == null)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return Begin.Next.Value;
        }

        public bool TryDequeue(out T? result)
        {
            if (Count == 0)
            {
                result = default;
                return false;
            }
            else
            {
                result = Dequeue();
                return true;
            }
        }

        public bool TryPeek(out T? result)
        {
            if (Count == 0)
            {
                result = default;
                return false;
            }
            else
            {
                result = Peek();
                return true;
            }
        }
    }
}