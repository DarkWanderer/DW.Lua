﻿using System.Collections.Generic;
using DW.Lua.Enumerators;

namespace DW.Lua.Extensions
{
    public static class EnumerableExtensions
    {
        public static INextAwareEnumerator<T> GetNextAwareEnumerator<T>(this IEnumerable<T> source)
        {
            return new NextAwareEnumerator<T>(source.GetEnumerator());
        } 
    }
}
