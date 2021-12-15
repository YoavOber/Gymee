using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GymeeDesktopApp.Models
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }= Visibility.Visible;
        public Visibility FalseValue { get; set; } = Visibility.Collapsed;

        public BoolToVisibilityConverter()
        {
            // set defaults
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }

    }

    public sealed class BoolToFillConverter : IValueConverter
    {
        public const string TrueValueHex = "#ff9b53";
        public const string FalseValueHex = "#959595";

        public const string OffBtnHex = "#2d2e2d";

        public SolidColorBrush TrueValue { get; set; }
        public SolidColorBrush FalseValue { get; set; }

        private readonly BrushConverter converter = new BrushConverter();
        public BoolToFillConverter()
        {
            // set defaults
            TrueValue = (SolidColorBrush)converter.ConvertFromString(TrueValueHex);
            FalseValue = (SolidColorBrush)converter.ConvertFromString(FalseValueHex);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }

    }

    public sealed class IntToFillConverter : IValueConverter
    {
        public const string TrueValueHex = "#FFF";
        public const string FalseValueHex = "#FF959595";

        public SolidColorBrush PosValue { get; set; }
        public SolidColorBrush NegValue { get; set; }

        private readonly BrushConverter converter = new BrushConverter();
        public IntToFillConverter()
        {
            // set defaults
            PosValue = (SolidColorBrush)converter.ConvertFromString(TrueValueHex);
            NegValue = (SolidColorBrush)converter.ConvertFromString(FalseValueHex);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return null;
            return (int)value > 0 ? PosValue : NegValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public sealed class StringToFillConverter : IValueConverter
    {
        public const string NewColorValueHex = "#FFF";
        public const string DefColorValueHex = "#FF959595";

        public SolidColorBrush DefaultColor { get; set; }
        public SolidColorBrush NewColor { get; set; }
        public string DefaultVal { get; set; }

        private readonly BrushConverter converter = new BrushConverter();
        public StringToFillConverter()
        {
            // set defaults
            DefaultColor = (SolidColorBrush)converter.ConvertFromString(DefColorValueHex);
            NewColor = (SolidColorBrush)converter.ConvertFromString(NewColorValueHex);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return null;
            return (string)value == DefaultVal ? DefaultColor : NewColor;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }

    }
    public sealed class StringToFlowDirection : IValueConverter
    {

        public FlowDirection DefaultDirection { get; set; }
        public FlowDirection NewDirection { get; set; }
        public string DefaultVal { get; set; }

        public StringToFlowDirection()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return null;
            return (string)value == DefaultVal ? DefaultDirection : NewDirection;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }

    }
    [ValueConversion(typeof(int), typeof(string))]
    public sealed class SelectionToStringConverter : IValueConverter
    {
        public bool OverrideDefault { get; set; } = false;
        public int IntValue { get; set; } = 0;
        public string DefaultString { get; set; }
        public string NewString { get; set; }

        public SelectionToStringConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return null;
            int val = (int)value;
            return val == IntValue ? DefaultString : OverrideDefault ? $"{NewString}" : $"{val} {NewString}";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }

    }



}