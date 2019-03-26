/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System;
using System.Globalization;
using System.Windows.Data;
using WPFTouchscreenKeyboard.Enums;

namespace WPFTouchscreenKeyboard.Converters
{
    public class EnumLayoutConverter : IValueConverter
    {
        static readonly InitialLayout _initialLayout = new InitialLayout();
        static readonly NumericLayout _numericLayout = new NumericLayout();


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return _initialLayout;


            LayoutType type = (LayoutType)value;

            if (type == LayoutType.Numeric)
                return _numericLayout;

            return _initialLayout;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
