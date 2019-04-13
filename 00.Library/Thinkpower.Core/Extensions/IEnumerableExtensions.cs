using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Thinkpower.Core
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T, int> action)
        {
            int index = 0;

            foreach (T item in enumeration)
            {
                action(item, index++);
            }
        }

        public static byte[] WriteAsFileBytes(this IEnumerable<string> enumeration, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream, encoding))
            {
                enumeration.ForEach((o) =>
                {
                    streamWriter.WriteLine(o);
                });

                streamWriter.Close();

                return stream.ToArray();
            }
        }

        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(
          this IEnumerable<TValue> values,
          Int32 chunkSize)
        {
            return values
                   .Select((v, i) => new { v, groupIndex = i / chunkSize })
                   .GroupBy(x => x.groupIndex)
                   .Select(g => g.Select(x => x.v));
        }
    }
}
