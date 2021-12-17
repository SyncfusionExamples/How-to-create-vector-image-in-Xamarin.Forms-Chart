# How-to-create-vector-image-in-Xamarin.Forms-Charts

Vector images can be scaled to any size without losing quality. This article explains how to add vector images in SfChart using the DataMarker LabelTemplate property.

You can achieve this requirement in two ways:

•	By adding SVG vector file directly into the DataMarker LabelTemplate property.

•	Drawing vector images using SkiaSharp.

![Add Vector image in SfChart](https://user-images.githubusercontent.com/63223423/146125113-832a6d6d-5516-4685-8bc6-491a229a8af0.png)

## Adding an SVG vector file directly into a DataMarker Template

### Step 1: 
Add the Xamarin.Forms.Svg NuGet package in the project and it provides support to add SvgImageSource to Xamarin.Forms ImageSource.

### Step 2: 
Initialize the SvgImage.Init to make it render in each platform-specific projects, as shown in the following code sample.

**Android [C#]**
```
protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
        …
    Xamarin.Forms.Svg.Droid.SvgImage.Init(this);
    LoadApplication(new App());
}
```

**iOS [C#]**
```
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
   {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            Xamarin.Forms.Svg.iOS.SvgImage.Init();
            SfChartRenderer.Init();
            return base.FinishedLaunching(app, options);
   }

```
In PCL: Add SvgImageSource.RegisterAssembly().

**[C#]**
```
protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
        …
    Xamarin.Forms.Svg.Droid.SvgImage.Init(this);
    LoadApplication(new App());
}
```
### Step 3: 
Add the image to your project and set the Build Action to Embedded Resource for your SVG image.

### Step 4: 
Define a HeightConverter to calculate the height for the image.

**[C#]**
```
public class HeightConverter : IValueConverter
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
```
Furthermore, in android, height is calculated using the screen density. For this purpose, it is necessary to implement the interface called IChartDependencyService.

**[C#]**
```
public interface IChartDependencyService
    {
        double GetDPI();
    } 
```
**Android[C#]**
```
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

```
**[C#]**
```
public interface IChartDependencyService
    {
        double GetDPI();
    } 
```

### Step 5: 
Add SvgImageSource to the ImageSource property of the image in DataMarker LabelTemplate and set the height and width for the image. Also, set the series color as Transparent to make the series disappear in the background. You can achieve this, as shown in the following code sample.

**[Xaml]**
```
<chart:SfChart.Resources>
        <local:Converter x:Key="heightConverter"/>
</chart:SfChart.Resources>
        ...
<chart:ColumnSeries DataMarkerPosition="Bottom" Color="Transparent" ItemsSource="{Binding Data}" XBindingPath="XValue" YBindingPath="YValue"> 
    <chart:ColumnSeries.DataMarker> 
        <chart:ChartDataMarker ShowLabel="True" LabelContent="DataMarkerLabel"> 
            <chart:ChartDataMarker.LabelTemplate> 
                <DataTemplate> 
                        <Image Aspect="Fill" HeightRequest="{Binding Converter={StaticResource heightConverter}, ConverterParameter={x:Reference chart}}" WidthRequest="55">
                            <Image.Source>
                                    <svg:SvgImageSource Source="Image1.svg" Width="100" Height="100" />
                            </Image.Source>
                        </Image>                            
                    </DataTemplate> 
            </chart:ChartDataMarker.LabelTemplate> 
            <chart:ChartDataMarker.LabelStyle> 
                <chart:DataMarkerLabelStyle LabelPosition="Inner"/> 
            </chart:ChartDataMarker.LabelStyle> 
        </chart:ChartDataMarker> 
    </chart:ColumnSeries.DataMarker> 
</chart:ColumnSeries> 
```
## Drawing the vector images using SkiaSharp

### Step 1: 
For setting up SkiaSharp, add SkiaSharp.Views.Forms NuGet package to your project.

### Step 2: 
Add an image to the project and set Build Action to Embedded Resource for your image.

### Step 3:
Refer to step 3 of how to add SVG vector file directly into the DataMarker LabelTemplate property to define the height converter that will be used to calculate the height for the image.
 
### Step 4: 
In the DataMarker LabelTemplate, add the SKCanvasView and set the series color as Transparent to make the series disappear in the background.

**[Xaml]**
```
<chart:SfChart x:Name="chart" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">

    <chart:SfChart.Resources>
        <local:Converter x:Key="heightConverter"/>
    </chart:SfChart.Resources>
    . . .
    <chart:SfChart.Series>
        <chart:ColumnSeries DataMarkerPosition="Bottom" Color="Transparent" ItemsSource="{Binding Data}" XBindingPath="XValue" YBindingPath="YValue">
            <chart:ColumnSeries.DataMarker>
                <chart:ChartDataMarker ShowLabel="True" LabelContent="DataMarkerLabel">
                    <chart:ChartDataMarker.LabelTemplate>
                        <DataTemplate>
                            <skia:SKCanvasView x:Name="canvasView" HeightRequest="{Binding Converter={StaticResource heightConverter}, ConverterParameter={x:Reference chart}}" WidthRequest="55"   PaintSurface="OnCanvasViewPaintSurface" />
                        </DataTemplate>
                    </chart:ChartDataMarker.LabelTemplate>
                    <chart:ChartDataMarker.LabelStyle>
                        <chart:DataMarkerLabelStyle LabelPosition="Inner"/>
                    </chart:ChartDataMarker.LabelStyle>
                </chart:ChartDataMarker>
            </chart:ColumnSeries.DataMarker>
        </chart:ColumnSeries>
    </chart:SfChart.Series>          
</chart:SfChart>
```
### Step 5: 
Load the image resource as SKBitmap from the assembly and draw the image on the canvas by using the PaintSurface event.

**[C#]**
```
public partial class MainPage : ContentPage
{
    static readonly SKBitmap originalBitmap =
        BitmapExtensions.LoadBitmapResource(typeof(MainPage),
            "ChartSample.Media.Image.png");

    public MainPage()
    {
        InitializeComponent();
    }

    void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
    {
        SKImageInfo info = args.Info;
        SKSurface surface = args.Surface;
        SKCanvas canvas = surface.Canvas;

        canvas.Clear();
        canvas.DrawBitmap(originalBitmap, info.Rect); //Draw own vector image. 
    }
}

static class BitmapExtensions
{
    public static SKBitmap LoadBitmapResource(Type type, string resourceID)
    {
        Assembly assembly = type.GetTypeInfo().Assembly;

        using (Stream stream = assembly.GetManifestResourceStream(resourceID))
        {
            return SKBitmap.Decode(stream);
        }
    }
}
```



