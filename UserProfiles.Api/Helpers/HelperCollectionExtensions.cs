﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Helpers
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
