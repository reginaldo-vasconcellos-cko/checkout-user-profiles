using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserProfiles.Common.Helpers
{
    public static class HelperCollectionExtensions
    {
        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }
    }
}
