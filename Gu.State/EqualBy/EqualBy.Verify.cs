﻿namespace Gu.State
{
    using System;
    using System.Reflection;

    public static partial class EqualBy
    {
        /// <summary>
        /// Check if the properties of <typeparamref name="T"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <typeparamref name="T"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <typeparam name="T">The type to get ignore properties for settings for</typeparam>
        /// <param name="referenceHandling">
        /// If Structural is used a deep equality check is performed.
        /// </param>
        /// <param name="bindingFlags">The binding flags to use when getting properties</param>
        public static void VerifyCanEqualByPropertyValues<T>(
            ReferenceHandling referenceHandling = ReferenceHandling.Structural,
            BindingFlags bindingFlags = Constants.DefaultPropertyBindingFlags)
        {
            var settings = PropertiesSettings.GetOrCreate(referenceHandling, bindingFlags);
            VerifyCanEqualByPropertyValues<T>(settings);
        }

        /// <summary>
        /// Check if the properties of <typeparamref name="T"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <typeparamref name="T"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <param name="settings">The settings to use.</param>
        public static void VerifyCanEqualByPropertyValues<T>(PropertiesSettings settings)
        {
            var type = typeof(T);
            VerifyCanEqualByPropertyValues(type, settings);
        }

        /// <summary>
        /// Check if the properties of <paramref name="type"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <paramref name="type"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="settings">The settings to use.</param>
        public static void VerifyCanEqualByPropertyValues(Type type, PropertiesSettings settings)
        {
            Verify.CanEqualByMemberValues(type, settings, typeof(EqualBy).Name, nameof(EqualBy.PropertyValues));
        }

        /// <summary>
        /// Check if the fields of <typeparamref name="T"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <typeparamref name="T"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <typeparam name="T">The type to get ignore fields for settings for</typeparam>
        /// <param name="referenceHandling">
        /// If Structural is used a deep equality check is performed.
        /// </param>
        /// <param name="bindingFlags">The binding flags to use when getting fields</param>
        public static void VerifyCanEqualByFieldValues<T>(
            ReferenceHandling referenceHandling = ReferenceHandling.Structural,
            BindingFlags bindingFlags = Constants.DefaultFieldBindingFlags)
        {
            var settings = FieldsSettings.GetOrCreate(referenceHandling, bindingFlags);
            VerifyCanEqualByFieldValues<T>(settings);
        }

        /// <summary>
        /// Check if the fields of <typeparamref name="T"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <typeparamref name="T"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <param name="settings">The settings to use.</param>
        public static void VerifyCanEqualByFieldValues<T>(FieldsSettings settings)
        {
            var type = typeof(T);
            VerifyCanEqualByFieldValues(type, settings);
        }

        /// <summary>
        /// Check if the fields of <paramref name="type"/> can be compared for equality
        /// This method will throw an exception if copy cannot be performed for <paramref name="type"/>
        /// Read the exception message for detailed instructions about what is wrong.
        /// Use this to fail fast or in unit tests.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="settings">The settings to use.</param>
        public static void VerifyCanEqualByFieldValues(Type type, FieldsSettings settings)
        {
            Verify.CanEqualByMemberValues(type, settings, typeof(EqualBy).Name, nameof(EqualBy.FieldValues));
        }

        internal static class Verify
        {
            internal static void CanEqualByMemberValues<T>(T x, T y, MemberSettings settings)
            {
                CanEqualByMemberValues(x, y, settings, typeof(EqualBy).Name, settings.EqualByMethodName());
            }

            internal static void CanEqualByMemberValues<T>(T x, T y, MemberSettings settings, string className, string methodName)
            {
                var type = x?.GetType() ?? y?.GetType() ?? typeof(T);
                CanEqualByMemberValues(type, settings, className, methodName);
            }

            internal static void CanEqualByMemberValues<T>(
                MemberSettings settings,
                string className,
                string methodName)
            {
                CanEqualByMemberValues(typeof(T), settings, className, methodName);
            }

            internal static void CanEqualByMemberValues(Type type, MemberSettings settings, string className, string methodName)
            {
                GetOrCreateErrors(type, settings)
                    .ThrowIfHasErrors(settings, className, methodName);
            }

            private static TypeErrors GetOrCreateErrors(Type type, MemberSettings settings, MemberPath path = null)
            {
                return settings.EqualByErrors.GetOrAdd(type, t => CreateErrors(t, settings, path));
            }

            private static TypeErrors CreateErrors(Type type, MemberSettings settings, MemberPath path)
            {
                if (settings.IsEquatable(type) || settings.TryGetComparer(type, out _))
                {
                    return null;
                }

                var errors = VerifyCore(settings, type)
                    .VerifyRecursive(type, settings, path, GetNodeErrors)
                    .Finnish();
                return errors;
            }

            private static ErrorBuilder.TypeErrorsBuilder VerifyCore(MemberSettings settings, Type type)
            {
                return ErrorBuilder.Start()
                                   .CheckRequiresReferenceHandling(type, settings, t => !settings.IsEquatable(t))
                                   .CheckIndexers(type, settings);
            }

            private static TypeErrors GetNodeErrors(MemberSettings settings, MemberPath path)
            {
                if (settings.ReferenceHandling == ReferenceHandling.References)
                {
                    return null;
                }

                var type = path.LastNodeType;
                return GetOrCreateErrors(type, settings, path);
            }
        }
    }
}
