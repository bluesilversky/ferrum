using System.Linq;

namespace Ferrum.Core.Extensions
{
    public static class LinqExtensions
    {
        public static bool In(this byte value, params byte[] values)
        {
            return values.Contains(value);
        }

        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains(value);
        }
    }
}
