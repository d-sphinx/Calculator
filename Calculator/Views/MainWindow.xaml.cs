using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.CalculatorViewModel();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size size = e.NewSize;

            if (size.Width > 800 && size.Height > 600)
            {
                gridLow.Visibility = Visibility.Hidden;
                gridHigh.Visibility = Visibility.Visible;
            }
            else
            {
                gridHigh.Visibility = Visibility.Hidden;
                gridLow.Visibility = Visibility.Visible;
            }
        }
    }
}
