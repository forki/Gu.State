﻿namespace Gu.State
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal static class ReferenceSetPool<T>
        where T : class
    {
        private static readonly ConcurrentQueue<HashSet<T>> Pool = new ConcurrentQueue<HashSet<T>>();

        internal static Disposer<HashSet<T>> Borrow()
        {
            HashSet<T> set;
            if (Pool.TryDequeue(out set))
            {
                return new Disposer<HashSet<T>>(set, Return);
            }

            return new Disposer<HashSet<T>>(new HashSet<T>(ReferenceComparer<T>.Default), Return);
        }

        private static void Return(HashSet<T> set)
        {
            set.Clear();
            Pool.Enqueue(set);
        }
    }
}