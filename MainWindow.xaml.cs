using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Ink;

namespace WpfControlAndApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.comboColors.SelectedIndex = 0;
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton)?.Content.ToString())
            {
                case "Ink Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorUsed = (this.comboColors.SelectedItem as ComboBoxItem)?.Content.ToString();
            this.MyInkCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorUsed);

        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            using var fs = new FileStream("StrokeData.bin", FileMode.Create);
            this.MyInkCanvas.Strokes.Save(fs);
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            using var fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read);
            StrokeCollection strokes = new StrokeCollection(fs);
            this.MyInkCanvas.Strokes = strokes;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            this.MyInkCanvas.Strokes.Clear();
        }
    }
}
