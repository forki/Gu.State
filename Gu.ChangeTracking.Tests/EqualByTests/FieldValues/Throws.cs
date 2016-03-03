﻿namespace Gu.ChangeTracking.Tests.EqualByTests.FieldValues
{
    using System;

    using Gu.ChangeTracking.Tests.EqualByTests;

    public class Throws : ThrowsTests
    {

        public override bool EqualByMethod<T>(T x, T y, ReferenceHandling referenceHandling = ReferenceHandling.Throw, string excludedMembers = null, Type excludedType = null)
        {
            var builder = FieldsSettings.Build();
            if (excludedMembers != null)
            {
                builder.AddIgnoredField<T>(excludedMembers);
            }

            if (excludedType != null)
            {
                builder.AddImmutableType(excludedType);
            }

            var settings = builder.CreateSettings(referenceHandling);
            return EqualBy.FieldValues(x, y, settings);
        }
    }
}
