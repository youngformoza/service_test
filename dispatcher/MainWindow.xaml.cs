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
        public IBaseCustomersRepository baseCustomersRepository = new CustomersRepository();
        public IBaseRequestsRepository baseRequestRepository = new RequestsRepository();
        public IBaseEquipmentRepository baseEquipmentRepository = new equipment_repository();
        public IBaseServicesRepository baseServicesRepository = new services_repository();
        public IBaseStatusRepository baseStatusRepository = new status_repository();

        public MainWindow()
        {
            InitializeComponent();
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }



        private void save_customer(object sender, RoutedEventArgs e)
        {
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);


            var updateCustomerDialog = new win_save_customer(baseCustomersRepository.GetById(chosenId));   // id получить из таблицы)

            updateCustomerDialog.ShowDialog();

            baseCustomersRepository.Update(updateCustomerDialog.UpdatingCustomer);

            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }

        private void add_customer(object sender, RoutedEventArgs e)
        {
            var addCustomerDialog = new win_add_customer(); 

            addCustomerDialog.ShowDialog();

            baseCustomersRepository.AddCustomer(addCustomerDialog.AddingCustomer);

            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();

        }

        private void del_customer(object sender, RoutedEventArgs e)
        {
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);
            var ChCus = baseCustomersRepository.GetById(chosenId);
            baseCustomersRepository.Delete(ChCus);
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }

        private void save_request(object sender, RoutedEventArgs e)
        {
            var updRequestDialog = new win_save_request();
            updRequestDialog.ShowDialog();
            var ChEq = baseEquipmentRepository.GetByName(updRequestDialog.ChEquipmentSeries);
            var ChSer = baseServicesRepository.GetByName(updRequestDialog.ChEquipmentService);
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);
            var ChCus = baseCustomersRepository.GetById(chosenId);
            var ChStat = baseStatusRepository.GetByName(updRequestDialog.ChEquipmentStatus);

            updRequestDialog.UpdatingRequest.cus = ChCus;
            updRequestDialog.UpdatingRequest.eq = ChEq;
            updRequestDialog.UpdatingRequest.ser = ChSer;
            updRequestDialog.UpdatingRequest.stat = ChStat;
        }

        private void add_request(object sender, RoutedEventArgs e)
        {
            var addRequestDialog = new win_add_request();
            addRequestDialog.ShowDialog();
            var ChEq = baseEquipmentRepository.GetByName(addRequestDialog.ChEquipmentSeries);
            var ChSer = baseServicesRepository.GetByName(addRequestDialog.ChEquipmentService);
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);
            var ChCus = baseCustomersRepository.GetById(chosenId);

            addRequestDialog.AddingRequest.cus = ChCus;
            addRequestDialog.AddingRequest.eq = ChEq;
            addRequestDialog.AddingRequest.ser = ChSer;
                        
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    => e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;


        private void find_customer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(customers_table.ItemsSource);
            if (filter == "") viewSource.Filter = null;
            else
            {
                viewSource.Filter = o =>
                {
                    Customer p = o as Customer;
                    return p.name.ToString().Contains(filter);
                };
                customers_table.ItemsSource = viewSource;
            }
        }

        private void find_request_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(customers_table.ItemsSource);
            if (filter == "") viewSource.Filter = null;
            else
            {
                viewSource.Filter = o =>
                {
                    DB_Connections.Entities.Request p = o as DB_Connections.Entities.Request;
                    return p.date_time_start.ToString().Contains(filter);
                };
                request_table.ItemsSource = viewSource;
            }

        }
    }

}
