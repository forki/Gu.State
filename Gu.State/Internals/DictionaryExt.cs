﻿namespace Gu.State
{
    using System;
    using System.Collections.Generic;

    internal static class DictionaryExt
    {
        internal static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TValue : IDisposable
        {
            TValue old;
            if (dictionary.TryGetValue(key, out old))
            {
                old?.Dispose();
            }

            dictionary[key] = value;
        }
    }
}