namespace PlaceHolder.Utils
{
    public static class LinqUtils
    {
        public static bool In<T>(this T item, IEnumerable<T> collection)
        {
            return collection.Contains(item);
        }

        public static bool In<T>(this T item, params T[] collection)
        {
            return collection.Contains(item);
        }
    }
}
