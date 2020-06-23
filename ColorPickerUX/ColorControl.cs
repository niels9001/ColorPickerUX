using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace ColorPickerUX
{
    public sealed class ColorControl : Control
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(ColorControl), new PropertyMetadata(""));
        public static readonly DependencyProperty ColorCodeProperty = DependencyProperty.Register(nameof(ColorCode), typeof(string), typeof(ColorControl), new PropertyMetadata(""));
        public static readonly DependencyProperty DescriptionWidthProperty = DependencyProperty.Register(nameof(DescriptionWidth), typeof(GridLength), typeof(ColorControl), new PropertyMetadata(""));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string ColorCode
        {
            get { return (string)GetValue(ColorCodeProperty); }
            set { SetValue(ColorCodeProperty, value); }
        }

        public GridLength DescriptionWidth
        {
            get { return (GridLength)GetValue(DescriptionWidthProperty); }
            set { SetValue(DescriptionWidthProperty, value); }
        }


        public ColorControl()
        {
            this.DefaultStyleKey = typeof(ColorControl);
        }
        Border ControlBorder;

        protected override void OnApplyTemplate()
        {
            ColorControl colorControl = (ColorControl)this;
            Grid CastGrid = (Grid)colorControl.GetTemplateChild("ShadowCastGrid");
            ThemeShadow Shadow = (ThemeShadow)colorControl.GetTemplateChild("BackdropShadow");

            ControlBorder = (Border)colorControl.GetTemplateChild("ControlBorder");
            ControlBorder.PointerEntered += ControlBorder_PointerEntered;
                ControlBorder.PointerExited += ControlBorder_PointerExited;
            ControlBorder.PointerPressed += ControlBorder_PointerPressed;

            Shadow.Receivers.Add(CastGrid);

            ColumnDefinition C = (ColumnDefinition)colorControl.GetTemplateChild("ColDef");
            C.Width = DescriptionWidth;
            base.OnApplyTemplate();


        }

        private void ControlBorder_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            OnClicked();
        }

        private void ControlBorder_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ControlBorder.Opacity = 1;
        }

        private void ControlBorder_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ControlBorder.Opacity = 0.8;
        }

        public event EventHandler Clicked;

        private void OnClicked()
        {
            Clicked?.Invoke(this, new PropertyChangedEventArgs("Slided"));
        }
    }
}
