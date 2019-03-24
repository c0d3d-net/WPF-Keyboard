/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System;
using System.Globalization;
using System.Windows.Data;
using Gazaloglu.OnScreenKeyboard.Enums;

namespace Gazaloglu.OnScreenKeyboard.Converters
{
    public class EnumLayoutConverter : IValueConverter
    {
        static readonly InitialLayout _initialLayout = new InitialLayout();
        static readonly NumericLayout _numericLayout = new NumericLayout();
        static readonly CapitalLayout _capitalLayout = new CapitalLayout();
        static readonly ShiftLayout   _shiftLayout   = new ShiftLayout();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            LayoutType type = (LayoutType)value;

            switch (type)
            {
                case LayoutType.Numeric:
                    return _numericLayout;

                case LayoutType.Capital:
                    return _capitalLayout;

                case LayoutType.Shift:
                    return _shiftLayout;

                default:
                    return _initialLayout;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
