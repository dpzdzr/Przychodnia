using Przychodnia.Features.Entities.PatientFeature.Models;
using System.Globalization;
using System.Windows.Data;

namespace Przychodnia.Shared.Converters;

public class SexToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Sex sex)
            return sex == Sex.Male ? "Mężczyzna" : "Kobieta";

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string sexString)
            return sexString == "Kobieta" ? Sex.Female : Sex.Male;

        return "";
    }
}
