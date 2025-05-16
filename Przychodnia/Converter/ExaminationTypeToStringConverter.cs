using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Przychodnia.Converter;
internal class ExaminationTypeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is ExaminationType type)
        {
            return type == ExaminationType.Physical ? "Fizyczne" : "Laboratoryjne";
        }
        return "";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is string typeString)
        {
            return typeString == "Fizyczne" ? ExaminationType.Physical : ExaminationType.Laboratory;
        }
        return "";
    }
}
