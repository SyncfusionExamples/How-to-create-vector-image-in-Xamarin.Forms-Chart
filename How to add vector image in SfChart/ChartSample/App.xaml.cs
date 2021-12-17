using System;
using Xamarin.Forms;
using Xamarin.Forms.Svg;
using Xamarin.Forms.Xaml;

namespace ChartSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SvgImageSource.RegisterAssembly();
            MainPage = new SVGImage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
