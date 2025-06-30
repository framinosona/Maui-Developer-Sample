using CommunityToolkit.Maui.Converters;

using System.Globalization;

namespace Maui_Developer_Sample.Pages.AppCapability.Converters;

[AcceptEmptyServiceProvider]
public class MillisecondsToSecondsStringConverter : BaseConverterOneWay<double, string>
{
    public override string ConvertFrom(double value, CultureInfo? culture)
    {
        var timespan= TimeSpan.FromMilliseconds(value);
        return $"{timespan.Seconds:D2}.{timespan.Milliseconds:D2}";
    }

    public override string DefaultConvertReturnValue { get; set; } = "";
}
