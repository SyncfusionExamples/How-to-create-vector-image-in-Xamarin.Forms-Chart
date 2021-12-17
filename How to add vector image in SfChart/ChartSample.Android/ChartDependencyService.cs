using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChartSample.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(ChartDependencyService))]
namespace ChartSample.Droid
{
    public class ChartDependencyService : IChartDependencyService
    {
        public double GetDPI()
        {
            return Application.Context.Resources.DisplayMetrics.Density;
        }
    }
}