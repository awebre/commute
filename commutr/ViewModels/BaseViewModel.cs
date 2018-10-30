using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace commutr.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 67


        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
