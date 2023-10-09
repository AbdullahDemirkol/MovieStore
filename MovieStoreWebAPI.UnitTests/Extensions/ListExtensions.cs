using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Extensions
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
