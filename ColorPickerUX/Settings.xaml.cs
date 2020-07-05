using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorPickerUX
{
    public sealed partial class Settings : Page
    {
        ObservableCollection<Snippet> Snippets { get; set; }
        public Settings()
        {
            this.InitializeComponent();
            Snippets = new ObservableCollection<Snippet>();

            Snippets.Add(new Snippet
            {
                Title = "HEX",
                Format = "#%5"
            });

            Snippets.Add(new Snippet
            {
                Title = "RGB",
                Format = "%1 %2 %3"
            });

            Snippets.Add(new Snippet
            {
                Title = "XAML",
                Format = "#FF%5"
            });

            Snippets.Add(new Snippet
            {
                Title = "WPF C#",
                Format = "Color.FromRGB(%1, %2, %3)"
            });

            Snippets.Add(new Snippet
            {
                Title = "HSL",
                Format = "%6° %7% %8%"
            });

            Snippets.Add(new Snippet
            {
                Title = "CSS RGB",
                Format = "rgb(%1, %2, %3)"
            });
        }

        private void SnippetDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Snippet S = new Snippet
            {
                Title = NameBox.Text,
                Format = FormatBox.Text
            };
            Snippets.Add(S);
        }

        private async void AddSizeButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = "";
            FormatBox.Text = "";
            await SnippetDialog.ShowAsync();
        }

        private async void ImagesSizesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Snippet S = (Snippet)e.ClickedItem;
            NameBox.Text = S.Title;
            FormatBox.Text = S.Format;
            await SnippetDialog.ShowAsync();
        }
    }

    public class Snippet
    {
        public string Title { get; set; }
        public string Format { get; set; }
    }
}
