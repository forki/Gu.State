﻿namespace Gu.State.Tests.Settings
{
    using System;

    using NUnit.Framework;

    using static SettingsTypes;

    public class MemberSettingsTests
    {
        [TestCase(typeof(int), true)]
        [TestCase(typeof(int?), true)]
        [TestCase(typeof(TimeSpan), true)]
        [TestCase(typeof(TimeSpan?), true)]
        [TestCase(typeof(StringSplitOptions), true)]
        [TestCase(typeof(StringSplitOptions?), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<int>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<int?>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithGetReadOnlyPropertySealed<int>>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertyStruct<WithGetReadOnlyPropertySealed<int>>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertyStruct<WithGetReadOnlyPropertyStruct<int>>), true)]
        [TestCase(typeof(WithReadonlyFieldSealed<int>), true)]
        [TestCase(typeof(WithReadonlyFieldSealed<int?>), true)]
        [TestCase(typeof(WithSelfFieldSealed), true)]
        [TestCase(typeof(WithSelfPropSealed), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithReadonlyField<int>>), false)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithGetReadOnlyProperty<int>>), false)]
        [TestCase(typeof(WithReadonlyField<WithGetReadOnlyProperty<int>>), false)]
        [TestCase(typeof(object), false)]
        [TestCase(typeof(int[]), false)]
        [TestCase(typeof(WithGetPrivateSet), false)]
        [TestCase(typeof(WithGetPublicSet), false)]
        [TestCase(typeof(WithMutableField), false)]
        [TestCase(typeof(WithSelfField), false)]
        [TestCase(typeof(WithSelfProp), false)]
        [TestCase(typeof(WithImmutableSubclassingMutable), false)]
        [TestCase(typeof(WithImmutableImplementingMutableInterfaceExplicit), false)]
        public void IsImmutable(Type type, bool expected)
        {
            var settings = PropertiesSettings.GetOrCreate();
            var isImmutable = settings.IsImmutable(type);
            Assert.AreEqual(expected, isImmutable);
        }

        [TestCase(typeof(int), true)]
        [TestCase(typeof(int?), true)]
        [TestCase(typeof(TimeSpan), true)]
        [TestCase(typeof(TimeSpan?), true)]
        [TestCase(typeof(StringSplitOptions), true)]
        [TestCase(typeof(StringSplitOptions?), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<int>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<int?>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithGetReadOnlyPropertySealed<int>>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertyStruct<WithGetReadOnlyPropertySealed<int>>), false)]
        [TestCase(typeof(WithGetReadOnlyPropertyStruct<WithGetReadOnlyPropertyStruct<int>>), false)]
        [TestCase(typeof(WithReadonlyFieldSealed<int>), false)]
        [TestCase(typeof(WithReadonlyFieldSealed<int?>), false)]
        [TestCase(typeof(WithSelfFieldSealed), false)]
        [TestCase(typeof(WithSelfPropSealed), false)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithReadonlyField<int>>), true)]
        [TestCase(typeof(WithGetReadOnlyPropertySealed<WithGetReadOnlyProperty<int>>), true)]
        [TestCase(typeof(WithReadonlyField<WithGetReadOnlyProperty<int>>), false)]
        [TestCase(typeof(object), false)]
        [TestCase(typeof(int[]), false)]
        [TestCase(typeof(WithGetPrivateSet), false)]
        [TestCase(typeof(WithGetPublicSet), false)]
        [TestCase(typeof(WithMutableField), false)]
        [TestCase(typeof(WithSelfField), false)]
        [TestCase(typeof(WithSelfProp), false)]
        [TestCase(typeof(WithImmutableSubclassingMutable), false)]
        [TestCase(typeof(WithImmutableImplementingMutableInterfaceExplicit), false)]
        public void IsEquatable(Type type, bool expected)
        {
            var settings = PropertiesSettings.GetOrCreate();
            var isEquatable = settings.IsEquatable(type);
            Assert.AreEqual(expected, isEquatable);
        }
    }
}