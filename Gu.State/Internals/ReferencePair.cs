namespace Gu.State
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct ReferencePair : IEquatable<ReferencePair>
    {
        internal readonly object X;
        internal readonly object Y;

        public ReferencePair(object x, object y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(ReferencePair left, ReferencePair right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReferencePair left, ReferencePair right)
        {
            return !Equals(left, right);
        }

        public bool Equals(ReferencePair other)
        {
            return ReferenceEquals(this.X, other.X) && ReferenceEquals(this.Y, other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ReferencePair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RuntimeHelpers.GetHashCode(this.X) * 397) ^ RuntimeHelpers.GetHashCode(this.Y);
            }
        }
    }
}