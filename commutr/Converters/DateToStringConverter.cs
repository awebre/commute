using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace commutr.Converters
{
    public class DateToStringConverter : IValueConverter, INotifyPropertyChanged
    {
        private string format = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Format
        {
            get => format;
            set => format = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime) value;
            return date.ToString(Format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse(value.ToString());
        }
    }
}