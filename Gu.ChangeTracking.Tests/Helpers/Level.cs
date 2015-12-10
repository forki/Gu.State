﻿namespace Gu.ChangeTracking.Tests.Helpers
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using JetBrains.Annotations;

    public class Level : INotifyPropertyChanged
    {
        private int value;
        private Level next;
        private ObservableCollection<int> ints = new ObservableCollection<int>();
        private ObservableCollection<Level> levels = new ObservableCollection<Level>();
        private string name;
        private StringComparison comparison;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Value
        {
            get { return this.value; }
            set
            {
                if (value == this.value)
                {
                    return;
                }
                this.value = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value == this.name)
                {
                    return;
                }
                this.name = value;
                OnPropertyChanged();
            }
        }

        public StringComparison Comparison
        {
            get { return this.comparison; }
            set
            {
                if (value == this.comparison)
                {
                    return;
                }
                this.comparison = value;
                OnPropertyChanged();
            }
        }

        public Level Next
        {
            get { return this.next; }
            set
            {
                if (Equals(value, this.next))
                {
                    return;
                }
                this.next = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> Ints
        {
            get { return this.ints; }
            set
            {
                if (Equals(value, this.ints))
                {
                    return;
                }
                this.ints = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Level> Levels
        {
            get { return this.levels; }
            set
            {
                if (Equals(value, this.levels))
                {
                    return;
                }
                this.levels = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
