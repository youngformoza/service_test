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
using dispatcher.Customers;
using dispatcher.Request;

namespace dispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void save_customer(object sender, RoutedEventArgs e)
        {
            var index = new win_save_customer();
            index.ShowDialog();
        }

        private void add_customer(object sender, RoutedEventArgs e)
        {
            var index = new win_add_customer();
            index.ShowDialog();
        }

        private void del_customer(object sender, RoutedEventArgs e)
        {
            var index = new win_del_customer();
            index.ShowDialog();
        }

        private void save_request(object sender, RoutedEventArgs e)
        {
            var index = new win_save_request();
            index.ShowDialog();
        }

        private void add_request(object sender, RoutedEventArgs e)
        {
            var index = new win_add_request();
            index.ShowDialog();
        }
    }
}
