﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Gazaloglu.OnScreenKeyboard.Enums;

namespace Gazaloglu.OnScreenKeyboard.Converters
{
    class EnumBackgroundConverter : IValueConverter
    {

        public Dictionary<LayoutType, object> BackgroundMap = new Dictionary<LayoutType, object>
        {
            [LayoutType.Initial] = new BitmapImage(new Uri("pack://application:,,/Gazaloglu.OnScreenKeyboard;component/Images/layout.png", UriKind.RelativeOrAbsolute)),
            [LayoutType.Capital] = new BitmapImage(new Uri("pack://application:,,/Gazaloglu.OnScreenKeyboard;component/Images/capitalLayout.png", UriKind.RelativeOrAbsolute)),
            [LayoutType.Shift]   = new BitmapImage(new Uri("pack://application:,,/Gazaloglu.OnScreenKeyboard;component/Images/shiftLayout.png", UriKind.RelativeOrAbsolute)),
            [LayoutType.Numeric] = new BitmapImage(new Uri("pack://application:,,/Gazaloglu.OnScreenKeyboard;component/Images/numberLayout.png", UriKind.RelativeOrAbsolute)),

        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                LayoutType type = (LayoutType)value;

                return BackgroundMap[type];
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}