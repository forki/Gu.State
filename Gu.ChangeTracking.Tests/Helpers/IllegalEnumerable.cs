﻿namespace Gu.ChangeTracking.Tests.Helpers
{
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public class IllegalEnumerable : INotifyPropertyChanged, IEnumerable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerator GetEnumerator()
        {
            return Enumerable.Empty<int>().GetEnumerator();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}