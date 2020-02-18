using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Views.EditViews
{
    /// <summary>
    /// Interaction logic for ExitExternalDBAttributes.xaml
    /// </summary>
    public partial class ExitExternalDBAttributes : Window
    {
        public ExitExternalDBAttributes()
        {
            InitializeComponent();
        }

        private void ApplyDatabaseParam_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
