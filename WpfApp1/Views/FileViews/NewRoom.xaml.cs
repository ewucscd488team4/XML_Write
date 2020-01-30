using System.Windows;

namespace WpfApp1.Views.FileViews
{
    /// <summary>
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {       

        public NewRoom()
        {
            InitializeComponent();
        }

        private void CloseNewRoom_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
