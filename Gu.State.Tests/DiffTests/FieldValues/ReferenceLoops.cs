﻿namespace Gu.State.Tests.DiffTests.FieldValues
{
    using System;

    public class ReferenceLoops : ReferenceLoopsTests
    {
        public override Diff DiffMethod<T>(T x, T y, ReferenceHandling referenceHandling = ReferenceHandling.Structural, string excludedMembers = null, Type excludedType = null)
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
            return DiffBy.FieldValues(x, y, settings);
        }
    }
}