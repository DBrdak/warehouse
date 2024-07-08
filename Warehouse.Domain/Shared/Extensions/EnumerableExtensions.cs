using System.Collections;

namespace Warehouse.Domain.Shared.Extensions;

public static class EnumerableExtensions
{
    public static int GetLength(this IEnumerable enumerable)
    {
            var enumerator = enumerable.GetEnumerator();
            var length = 0;
            enumerator.MoveNext();
            var isCurrentExists = enumerator.MoveNext();

            if (!isCurrentExists)
            {
                return length;
            }

            do
            {
                length++;
            }
            while (enumerator.MoveNext());
            
            return length;
        }

    public static int IndexOf(this IEnumerable enumerable, object item)
    {
            var enumerator = enumerable.GetEnumerator();
            var length = 0;
            enumerator.MoveNext();
            var isCurrentExists = enumerator.MoveNext();

            if (!isCurrentExists)
            {
                return -1;
            }

            do
            {
                length++;
                if (item.Equals(enumerator.Current))
                {
                    return length;
                }
            }
            while (enumerator.MoveNext());
            
            return -1;
        }

    public static T? Find<T>(this IEnumerable enumerable, object item)
    {
            var enumerator = enumerable.GetEnumerator();
            var isCurrentExists = enumerator.MoveNext();

            if (!isCurrentExists)
            {
                return default;
            }

            do
            {
                if (item.Equals(enumerator.Current))
                {
                    return (T)enumerator.Current ?? throw new InvalidCastException();
                }
            }
            while (enumerator.MoveNext());

            return default;
        }

    public static IEnumerable Replace(this IEnumerable enumerable, object oldItem, object newItem)
    {
            var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            var newEnumerable = new List<object>();
            var isCurrentExists = enumerator.MoveNext();

            if (!isCurrentExists)
            {
                return default;
            }

            do
            {
                if (oldItem.Equals(enumerator.Current))
                {
                    newEnumerable.Add(newItem);
                }
                newEnumerable.Add(enumerator.Current);
            }
            while (enumerator.MoveNext());

            return newEnumerable;
        }
}