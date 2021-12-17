using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChartSample
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SfChart chart = parameter as SfChart;
            ChartDataMarkerLabel dataMarkerLabel = value as ChartDataMarkerLabel;


            if (chart != null && dataMarkerLabel != null)
            {
                var height = chart.ValueToPoint(chart.SecondaryAxis, (dataMarkerLabel.Data as Model).YValue);

                if (Device.RuntimePlatform == Device.Android)
                {
                    double DPI = DependencyService.Get<IChartDependencyService>().GetDPI();
                    return (chart.SeriesBounds.Height - height) / DPI;
                }
                else
                {
                    return chart.SeriesBounds.Height - height;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                var age = int.Parse(value.ToString()); 

                if(age == 12)
                {
                    return "ChartSample.Media.Age12.svg";
                }
                else if (age == 14)
                {
                    return "ChartSample.Media.Age14.svg";
                }
                else if (age == 16)
                {
                    return "ChartSample.Media.Age16.svg";
                }
                else if (age == 18)
                {
                    return "ChartSample.Media.Age18.svg";
                }
            }                     
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
