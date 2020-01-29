using System.Windows;

namespace WpfApp1.Views.WarningViews
{
    /// <summary>
    /// Interaction logic for UpdateNotImplemented.xaml
    /// </summary>
    public partial class UpdateNotImplemented : Window
    {
        public UpdateNotImplemented()
        {
            InitializeComponent();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //closes the Window, all other processing happens in the view model
            this.Close();
        }
    }
}
