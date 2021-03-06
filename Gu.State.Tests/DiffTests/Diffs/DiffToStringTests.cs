﻿namespace Gu.State.Tests.DiffTests
{
    using NUnit.Framework;

    using static DiffTypes;

    public class DiffToStringTests
    {
        [TestCase("a", "b", "StringValue x: a y: b")]
        [TestCase(null, "b", "StringValue x: null y: b")]
        public void PropertyDiff(string x, string y, string expected)
        {
            var diff = new PropertyDiff(typeof(WithSimpleProperties).GetProperty(nameof(WithSimpleProperties.StringValue)), x, y);
            Assert.AreEqual(expected, diff.ToString(string.Empty, " "));
        }
    }
}
