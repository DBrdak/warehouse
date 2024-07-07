using System.Collections;

namespace Warehouse.Domain.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static int GetLength(this IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            var length = 0;
            enumerator.MoveNext();

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
    }
}
