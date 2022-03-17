namespace FirstLibrary
{
    using System;
    using System.Collections.Generic;

    public class Stack<T> : Container<T>
    {
        public Stack() : base() { }

        public Stack(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Push(item);
            }
        }

        public void Push(T value)
        {
            var item = new Item<T>
            {
                Next = Begin.Next,
                Value = value
            };

            Begin.Next = item;
            Count++;
        }

        public T Peek()
        {
            if (Count == 0 || Begin.Next == null || Begin.Next.Value == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            return Begin.Next.Value;
        }

        public T Pop()
        {
            if (Count == 0 || Begin.Next == null || Begin.Next.Value == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            var currentFirst = Begin.Next;

            var value = currentFirst.Value;

            Begin.Next = currentFirst.Next;
            Count--;

            return value;
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

        public bool TryPop(out T? result)
        {
            if (Count == 0)
            {
                result = default;
                return false;
            }
            else
            {
                result = Pop();
                return true;
            }
        }
    }
}