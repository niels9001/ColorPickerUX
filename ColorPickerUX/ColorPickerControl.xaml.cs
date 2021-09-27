
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Extensions;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;
using Windows.UI.Xaml.Shapes;

namespace ColorPickerUX
{
    public sealed partial class ColorPickerControl : UserControl
    {
        public ObservableCollection<Color> PickedColors { get; set; }

        public ColorPickerControl()
        {
            this.InitializeComponent();

            PickedColors = new ObservableCollection<Color>();

        }

        private async void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            var eyedropper = new Eyedropper();
            eyedropper.PickCompleted += Eyedropper_PickCompleted;
            var color = await eyedropper.Open();
        }

        private void AddColor(Color C)
        {
            PickedColors.Insert(0, C);
            ColorsListView.SelectedIndex = 0;
        }
        private void Eyedropper_PickCompleted(Eyedropper sender, EventArgs args)
        {
            AddColor(sender.Color);
       
        }

        private void ColorsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var y = (sender as ListView).SelectedItem;
            if (y != null)
            {
                Color textBlock = (Color)(sender as ListView).SelectedItem;
                int SelectedIndex = PickedColors.IndexOf(textBlock);

                RenderColor(textBlock);


            }

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {



        }

        private void RenderColor(Color SelectedColor)
        {
            ColorPicker.Color = SelectedColor;
            ColorRect.Background = new SolidColorBrush(SelectedColor);

            List<ColorCode> ColorFormats = new List<ColorCode>();
            ColorFormats.Add(new ColorCode() { Name = "HEX", Format = "#" + SelectedColor.ToHex().Remove(0, 3) });
            ColorFormats.Add(new ColorCode() { Name = "RGB", Format = SelectedColor.R + "  " + SelectedColor.G + "  " + SelectedColor.B });

            HslColor SelectedHSLColor = SelectedColor.ToHsl();
            ColorFormats.Add(new ColorCode() { Name = "HSL", Format = Math.Round(SelectedHSLColor.H, 0) + "°  " + Math.Round(SelectedHSLColor.S, 0) + "%  " + Math.Round(SelectedHSLColor.L, 0) + "%" });

            HsvColor SelectedHSVColor = SelectedColor.ToHsv();
            ColorFormats.Add(new ColorCode() { Name = "HSB", Format = Math.Round(SelectedHSVColor.H, 0) + "°  " + Math.Round(SelectedHSVColor.S, 0) + "%  " + Math.Round(SelectedHSVColor.V, 0) + "%" });

            ColorFormats.Add(new ColorCode() { Name = "XAML", Format = SelectedColor.ToHex() });
            ColorFormats.Add(new ColorCode() { Name = "C#", Format = "new Color() { R = " + SelectedColor.R + ", G = " + SelectedColor.G + ", B = " + SelectedColor.B + "}" });

            ColorFormats.Add(new ColorCode() { Name = "CSS HEX", Format = "#" + SelectedColor.ToHex().Remove(0, 3) });
            ColorFormats.Add(new ColorCode() { Name = "CSS RGB", Format = "rgb(" + SelectedColor.R + ", " + SelectedColor.G + ", " + SelectedColor.B + ")" });
            ColorFormats.Add(new ColorCode() { Name = "CSS HSL", Format = "hsl(" + SelectedColor.R + ", " + SelectedColor.G + "%, " + SelectedColor.B + "%)" });
            ColorFormatsList.ItemsSource = ColorFormats;


            Icon.Foreground = new SolidColorBrush(SelectedColor);
            Gradient1.Background = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Max(SelectedHSVColor.V + 0.3)));
            Gradient2.Background = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Max(SelectedHSVColor.V + 0.15)));
            Gradient3.Background = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Min(SelectedHSVColor.V - 0.2)));
            Gradient4.Background = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Min(SelectedHSVColor.V - 0.3)));

        }


        private double Max(double Value)
        {
            if (Value > 1)
            {
                Value = 1;

            }
            return Value;
        }

        private double Min(double Value)
        {
            if (Value < 0)
            {
                Value = 0;

            }
            return Value;
        }

        private void ColorPicker_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            AddColor(ColorPicker.Color);
        }
        DispatcherTimer Timer = new DispatcherTimer();
        private void CopyToClipboard(object sender, EventArgs e)
        {
            CopyToClipboardBanner.Visibility = Visibility.Visible;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 2);
            Timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            CopyToClipboardBanner.Visibility = Visibility.Collapsed;
            Timer.Stop();
        }

        private void Gradient_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Border R = sender as Border;

            AddColor(((SolidColorBrush)R.Background).Color);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AddColor(new Color() { R = 134, G = 163, B = 195, A = 255 }); ;
            AddColor(new Color() { R = 182, G = 206, B = 199, A = 255 });

            AddColor(new Color() { R = 3, G = 63, B = 99, A = 255 });
            AddColor(new Color() { R = 40, G = 102, B = 110, A = 255 });

            AddColor(new Color() { R = 124, G = 152, B = 133, A = 255 });

            }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ColorCode
    {
        public string Name { get; set; }
        public string Format { get; set; }
    }
}
