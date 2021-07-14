using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using dispatcher.Customers;
using dispatcher.Request;
using service_center.repositories;

namespace dispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBaseCustomersRepository baseCustomersRepository = new customers_repository();
        public IBaseRequestsRepository baseRequestRepository = new requests_repository();
        public MainWindow()
        {
            InitializeComponent();
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
            AllocConsole();
        }

        [DllImport("Kernel32.dll")]
        static extern void AllocConsole();


        private void save_customer(object sender, RoutedEventArgs e)
        {
            var index = new win_save_customer();
            index.ShowDialog();
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }

        private void add_customer(object sender, RoutedEventArgs e)
        {
            //List<customer> all_customers = new List<customer>();
            var index = new win_add_customer();
            index.ShowDialog();
            //var taken_customers =  baseCustomersRepository.GetCustomers();
            //foreach (customer ch_customer in taken_customers)
            //{
            //    all_customers.Add(ch_customer);
            //}

            //customers_table.ItemsSource = all_customers;
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }

        private void del_customer(object sender, RoutedEventArgs e)
        {
            var index = new win_del_customer();
            index.ShowDialog();
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
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

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    => e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
    }

}
