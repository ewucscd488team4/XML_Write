using System.Windows;
using SAUSALibrary.Defaults;
using SAUSALibrary.FileHandling.XML.Writing;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {
        private string projectFile = "Hangerbay.xml";
        public NewRoom()
        {            
            InitializeComponent();
        }

        public void CloseNewRoom_Click(object sender, RoutedEventArgs e)
        {
            //closes the window
            this.Close();
        }

        public void SetRoomDimensions(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(rLength.Text) || string.IsNullOrEmpty(rWidth.Text) || string.IsNullOrEmpty(rHeight.Text) || string.IsNullOrEmpty(rWeight.Text)) {
                //launch error dialog for enpty fields
            } else
            {
                string[] dimensions = new string[4];
                dimensions[0] = rLength.Text;
                dimensions[1] = rWidth.Text;
                dimensions[2] = rHeight.Text;
                dimensions[3] = rWeight.Text;
                WriteXML.SaveDimensions(FilePathDefaults.ScratchFolder + projectFile, dimensions);
            }            
        }
    }
}
