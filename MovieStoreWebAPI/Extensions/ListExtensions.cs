using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Extensions
{
    public static class ListExtensions
    {
        public static List<T> AddListRange<T>(this List<T> lists, params List<T>[] addValues)
        {
            if (addValues != null)
            {
                foreach (var addValue in addValues)
                {
                    if (addValue != null)
                        lists.AddRange(addValue);
                }
            }
            return lists;
        }
    }
}
