﻿namespace Gu.ChangeTracking.Tests.EqualByTests.PropertyValues
{
    using System;

    using Gu.ChangeTracking.Tests.EqualByTests;

    public class Throws : ThrowsTests
    {
        public override bool EqualByMethod<T>(T x, T y, ReferenceHandling referenceHandling = ReferenceHandling.Throw, string excludedMembers = null, Type excludedType = null)
        {
            var builder = PropertiesSettings.Build();
            if (excludedMembers != null)
            {
                builder.AddIgnoredProperty<T>(excludedMembers);
            }

            if (excludedType != null)
            {
                builder.AddImmutableType(excludedType);
            }

            var settings = builder.CreateSettings(referenceHandling);
            return EqualBy.PropertyValues(x, y, settings);
        }
    }
}
