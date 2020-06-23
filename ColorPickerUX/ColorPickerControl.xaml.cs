
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
            BackdropShadow.Receivers.Add(ShadowCastingGrid);
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
            HEX.ColorCode = "#" + SelectedColor.ToHex().Remove(0, 3);
            RGB.ColorCode = SelectedColor.R + "  " + SelectedColor.G + "  " + SelectedColor.B;

            HslColor SelectedHSLColor = SelectedColor.ToHsl();
            HSL.ColorCode = Math.Round(SelectedHSLColor.H, 0) + "°  " + Math.Round(SelectedHSLColor.S, 0) + "%  " + Math.Round(SelectedHSLColor.L, 0) + "%";

            HsvColor SelectedHSVColor = SelectedColor.ToHsv();
            HSB.ColorCode = Math.Round(SelectedHSVColor.H, 0) + "°  " + Math.Round(SelectedHSVColor.S, 0) + "%  " + Math.Round(SelectedHSVColor.V, 0) + "%";

            XAML.ColorCode = SelectedColor.ToHex();
            WPF.ColorCode = "Color.FromRGB(" + SelectedColor.R + ", " + SelectedColor.G + ", " + SelectedColor.B + ")";
            UWP.ColorCode = "new Color() { R = " + SelectedColor.R + ", G = " + SelectedColor.G + ", B = " + SelectedColor.B + "}";
            Maui.ColorCode = "Color.FromRGB(" + SelectedColor.R + ", " + SelectedColor.G + ", " + SelectedColor.B + ")";

            CSSHEX.ColorCode = "#" + SelectedColor.ToHex().Remove(0, 3);
            CSSRGB.ColorCode = "rgb(" + SelectedColor.R + ", " + SelectedColor.G + ", " + SelectedColor.B + ")";
            CSSHSL.ColorCode = "hsl(" + SelectedColor.R + ", " + SelectedColor.G + "%, " + SelectedColor.B + "%)";


            Gradient1.Fill = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Max(SelectedHSVColor.V + 0.3)));
            Gradient2.Fill = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Max(SelectedHSVColor.V + 0.15)));
            Gradient3.Fill = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Min(SelectedHSVColor.V - 0.2)));
            Gradient4.Fill = new SolidColorBrush(ColorHelper.FromHsv(SelectedHSVColor.H, SelectedHSVColor.S, Min(SelectedHSVColor.V - 0.3)));

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
            Rectangle R = sender as Rectangle;

            AddColor(((SolidColorBrush)R.Fill).Color);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AddColor(new Color() { R = 134, G = 163, B = 195, A = 255 }); ;
            AddColor(new Color() { R = 182, G = 206, B = 199, A = 255 });
        }
    }
}
