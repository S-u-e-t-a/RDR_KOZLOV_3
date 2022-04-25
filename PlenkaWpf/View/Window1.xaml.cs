using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using PlenkaWpf.Utils;
using PlenkaWpf.VM;
using MessageBox = HandyControl.Controls.MessageBox;
using Separator = LiveCharts.Wpf.Separator;

namespace PlenkaWpf.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            var vm = new Window1VM();
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }


        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual2(visual, fileName, encoder);
        }

        private static void EncodeVisual2(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }


        public byte[] EncodeVisual(FrameworkElement element,int dpi)
        {
            var bitmap = new RenderTargetBitmap(((int)element.ActualWidth * dpi) / 96, (((int)element.ActualHeight + 50) * dpi) / 96, dpi, dpi
                , PixelFormats.Pbgra32);
            bitmap.Render(element);
            var frame = BitmapFrame.Create(bitmap);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                var bit = stream.ToArray();
                stream.Close();

                return bit;
            }
        }

        private CartesianChart copyChart(CartesianChart chartToCopy, double width, double height)
        {
            var copiedChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = width,
                Height = height,
                Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = chartToCopy.Series[0].Values
                    }
                },
                AxisX = new AxesCollection()
                {
                    new Axis()
                    {
                        Separator = new Separator()
                        {
                            Step = chartToCopy.AxisX[0].Separator.Step,
                            Stroke= chartToCopy.AxisX[0].Separator.Stroke
                        },
                        Title = chartToCopy.AxisX[0].Title,
                        MinValue = chartToCopy.AxisX[0].MinValue
                    }
                },
                AxisY = new AxesCollection()
                {
                    new Axis()
                    {
                        Separator = new Separator()
                        {
                            Step = chartToCopy.AxisY[0].Separator.Step,
                            Stroke= chartToCopy.AxisY[0].Separator.Stroke
                        },
                        Title = chartToCopy.AxisY[0].Title,
                        MinValue = chartToCopy.AxisY[0].MinValue
                    }
                }
            };

            return copiedChart;
        }

        private byte[] chartToBitmap(CartesianChart chart, double width = 1000, double height = 1000, int dpi = 150)
        {
            var nonVisibleChart = copyChart(chart, width, height);
            var viewbox = new Viewbox();
            viewbox.Child = nonVisibleChart;
            viewbox.Measure(nonVisibleChart.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), nonVisibleChart.RenderSize));
            nonVisibleChart.Update(true, true); //force chart redraw
            viewbox.UpdateLayout();
            return EncodeVisual(nonVisibleChart, dpi);
        }
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.FileName = "АНАЛИЗ_" + DateTime.Now.ToString().Replace(':', '_');
            var res = dlg.ShowDialog();
            if (res == true)
            {
                if ((DataContext as Window1VM).IsCalculated )
                {

                    var tempChartBitMap = chartToBitmap(tempChart);

                    var nChartBitMap = chartToBitmap(nChart);

                    
                    FileSystem.exportPdf(dlg.FileName, tempChartBitMap,nChartBitMap, (DataContext as Window1VM).MathClass);
                }
                else
                {
                    MessageBox.Show("Нет данных для сохранения", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // нарушение mvvm
        {
            if (!IsValid(MainGrid))
            {
                MessageBox.Show("Невозможно произвести расчет, есть ошибки ввода данных");
            }
            else
            {
                (DataContext as Window1VM).CalcCommand.Execute(null);
            }
        }



        private bool IsValid(DependencyObject obj)
         {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
                   LogicalTreeHelper.GetChildren(obj)
                       .OfType<DependencyObject>()
                       .All(IsValid);
        }
        private void Validation_OnError(object? sender, ValidationErrorEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}