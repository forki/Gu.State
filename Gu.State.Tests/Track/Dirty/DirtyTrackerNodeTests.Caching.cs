// ReSharper disable RedundantArgumentDefaultValue
namespace Gu.State.Tests
{
    using System;

    using NUnit.Framework;

    using static DirtyTrackerTypes;

    public class DirtyTrackerNodeTests
    {
        public class Caching
        {
            [Test]
            public void ReturnsSameWhileAlive()
            {
                var x = new WithSimpleProperties();
                var y = new WithSimpleProperties();
                var settings = PropertiesSettings.GetOrCreate(ReferenceHandling.Structural);
                var t1 = DirtyTrackerNode.GetOrCreate(x, y, settings, isRoot: true);
                var t2 = DirtyTrackerNode.GetOrCreate(x, y, settings, isRoot: true);
                Assert.AreSame(t1, t2);
                t1.Dispose();
                using (var t3 = DirtyTrackerNode.GetOrCreate(x, y, settings, isRoot: true))
                {
                    Assert.AreSame(t1, t3);
                    t2.Dispose();
                    t3.Dispose();

                    using (var t4 = DirtyTrackerNode.GetOrCreate(x, y, settings, isRoot: true))
                    {
                        Assert.AreNotSame(t1, t4);
                    }
                }
            }

            [Test]
            public void ReturnsDifferentForDifferentPairs()
            {
                var x = new WithSimpleProperties { Value = 1, Time = DateTime.MinValue };
                var y = new WithSimpleProperties { Value = 1, Time = DateTime.MinValue };
                var settings = PropertiesSettings.GetOrCreate(ReferenceHandling.Structural);
                using (var t1 = DirtyTrackerNode.GetOrCreate(x, y, settings, isRoot: true))
                {
                    using (var t2 = DirtyTrackerNode.GetOrCreate(y, x, settings, isRoot: true))
                    {
                        Assert.AreNotSame(t1, t2);
                    }
                }
            }

            [Test]
            public void ReturnsDifferentForDifferentSettings()
            {
                var x = new WithSimpleProperties { Value = 1, Time = DateTime.MinValue };
                var y = new WithSimpleProperties { Value = 1, Time = DateTime.MinValue };
                using (var t1 = DirtyTrackerNode.GetOrCreate(x, y, PropertiesSettings.GetOrCreate(ReferenceHandling.Structural), isRoot: true))
                {
                    using (var t2 = DirtyTrackerNode.GetOrCreate(x, y, PropertiesSettings.GetOrCreate(ReferenceHandling.References), isRoot: true))
                    {
                        Assert.AreNotSame(t1, t2);
                    }
                }
            }
        }
    }
}