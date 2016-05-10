﻿namespace Gu.State
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class SetDiffBy<T> : SetDiffBy, IDiffBy
    {
        public static readonly SetDiffBy<T> Default = new SetDiffBy<T>();

        private SetDiffBy()
        {
        }

        public void AddDiffs<TSettings>(
            DiffBuilder collectionBuilder,
            object x,
            object y,
            TSettings settings,
            Action<DiffBuilder, object, object, object, TSettings> itemDiff)
            where TSettings : IMemberSettings
        {
            this.AddDiffs(collectionBuilder, (ISet<T>)x, (ISet<T>)y, settings, itemDiff);
        }

        private static void AddItemDiffs(DiffBuilder collectionBuilder, ISet<T> x, ISet<T> y, HashSet<T> borrow)
        {
            borrow.UnionWith(x);
            if (borrow.SetEquals(y))
            {
                return;
            }

            borrow.ExceptWith(y);
            foreach (var xi in borrow)
            {
                collectionBuilder.Add(new IndexDiff(xi, new ValueDiff(xi, PaddedPairs.MissingItem)));
            }

            borrow.Clear();
            borrow.UnionWith(y);
            borrow.ExceptWith(x);
            foreach (var yi in borrow)
            {
                collectionBuilder.Add(new IndexDiff(yi, new ValueDiff(PaddedPairs.MissingItem, yi)));
            }
        }

        private void AddDiffs<TSettings>(
            DiffBuilder collectionBuilder,
            ISet<T> x,
            ISet<T> y,
            TSettings settings,
            // ReSharper disable once UnusedParameter.Local
            Action<DiffBuilder, object, object, object, TSettings> itemDiff)
                where TSettings : IMemberSettings
        {
            if (typeof(T).Implements<IEquatable<T>>())
            {
                using (var borrow = SetPool<T>.Borrow(EqualityComparer<T>.Default.Equals, EqualityComparer<T>.Default.GetHashCode))
                {
                    AddItemDiffs(collectionBuilder, x, y, borrow.Value);
                    return;
                }
            }

            switch (settings.ReferenceHandling)
            {
                case ReferenceHandling.Throw:
                    throw Throw.ShouldNeverGetHereException("ReferenceHandling should be checked before");
                case ReferenceHandling.References:
                    using (var borrow = SetPool<T>.Borrow((xi, yi) => ReferenceEquals(xi, yi), item => RuntimeHelpers.GetHashCode(item)))
                    {
                        AddItemDiffs(collectionBuilder, x, y, borrow.Value);
                        return;
                    }

                case ReferenceHandling.Structural:
                    using (var borrow = SetPool<T>.Borrow((xi, yi) => EqualBy.MemberValues(xi, yi, settings), xi => 0))
                    {
                        AddItemDiffs(collectionBuilder, x, y, borrow.Value);
                        return;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}