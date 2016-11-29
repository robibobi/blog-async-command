using System.Windows;
using System.Windows.Input;

namespace Tcoc.Blog.Async
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Nur Zahlen zulassen 
        private void PrimeCountTBPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
