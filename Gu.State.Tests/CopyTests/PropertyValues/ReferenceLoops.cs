﻿namespace Gu.State.Tests.CopyTests.PropertyValues
{
    public class ReferenceLoops : CopyTests.ReferenceLoops
    {
        public override void CopyMethod<T>(
            T source,
            T target,
            ReferenceHandling referenceHandling = ReferenceHandling.Structural,
            string excluded = null)
        {
            var builder = PropertiesSettings.Build();
            if (excluded != null)
            {
                builder.IgnoreProperty<T>(excluded);
            }

            //if (excludedType != null)
            //{
            //    builder.IgnoreType(excludedType);
            //}

            var settings = builder.CreateSettings(referenceHandling);
            Copy.PropertyValues(source, target, settings);
        }
    }
}
