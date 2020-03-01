using System.Windows;

namespace WpfApp1.Views.FileViews
{
    /// <summary>
    /// Interaction logic for ExportProject.xaml
    /// </summary>
    public partial class ExportProject : Window
    {
        public ExportProject()
        {
            InitializeComponent();
        }

        private void ApplyPrefs_Click(object sender, RoutedEventArgs e)
        {
            //closes the Window, all other processing happens in the view model
            this.Close();
        }
    }
}
