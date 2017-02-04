using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WcoeJobFairRegistration
{
    // https://github.com/jamesmontemagno/mvvm-helpers/blob/master/MvvmHelpers/ObservableObject.cs
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null, Action onChanged = null)
        {
            if(object.Equals(storage, value))
                return false;

            storage = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}