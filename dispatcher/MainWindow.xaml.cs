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
using DB_service_Infrastructure.MySQLRepositories;
using service_center.repositories;

namespace dispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    /*  //  public IBaseCustomersRepository baseCustomersRepository = new CustomersRepository();
        public IBaseRequestsRepository baseRequestRepository = new RequestsRepository();
        public IBaseEquipmentRepository baseEquipmentRepository = new equipment_repository();
        public IBaseServicesRepository baseServicesRepository = new services_repository();
        public IBaseStatusRepository baseStatusRepository = new status_repository();*/

        public IBaseCustomersRepository baseCustomersRepository = new MySQLCustomersRepository();
        public IBaseRequestsRepository baseRequestRepository = new MySQLRequestsRepository();
        public IBaseEquipmentRepository baseEquipmentRepository = new MySQLEquipmentRepository();
        public IBaseEmployeesRepository baseEmployeesRepository = new MySQLEmployeesRepository();
        public IBaseServicesRepository baseServicesRepository = new MySQLServicesRepository();
        public IBaseStatusRepository baseStatusRepository = new MySQLStatusRepository();


        public MainWindow()
        {
            InitializeComponent();
            //var customers = new List<Customer>(baseCustomersRepository.GetCustomers());
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
            //var customers = new List<Customer>();
            //customers.Add(baseCustomersRepository.GetById(1));
            //customers_table.ItemsSource = customers;
        }



        private void save_customer(object sender, RoutedEventArgs e)
        {
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);


            var updateCustomerDialog = new win_save_customer(baseCustomersRepository.GetById(chosenId));   

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
            var chosenIdRequest = baseRequestRepository.GetId(request_table.SelectedIndex);
            var ChRequest = baseRequestRepository.GetById(chosenIdRequest);

            var updRequestDialog = new win_save_request(ChRequest);
            updRequestDialog.ShowDialog();

            
            var ChEq = baseEquipmentRepository.GetByName(updRequestDialog.ChEquipmentSeries);
            var ChSer = baseServicesRepository.GetByName(updRequestDialog.ChEquipmentService);
            var chosenIdCustomer = baseCustomersRepository.GetId(customers_table.SelectedIndex);
            var ChCus = baseCustomersRepository.GetById(chosenIdCustomer);
            var ChStat = baseStatusRepository.GetByName(updRequestDialog.ChEquipmentStatus);

            updRequestDialog.UpdatingRequest.cus = ChCus;
            updRequestDialog.UpdatingRequest.eq = ChEq;
            updRequestDialog.UpdatingRequest.ser = ChSer;
            updRequestDialog.UpdatingRequest.stat = ChStat;

            baseRequestRepository.Update(updRequestDialog.UpdatingRequest);
        }

        private void add_request(object sender, RoutedEventArgs e)
        {
            var addRequestDialog = new win_add_request();
            
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);
            var ChCus = baseCustomersRepository.GetById(chosenId);
            var ChReception = baseEmployeesRepository.GetById(1);
            var ChStatus = baseStatusRepository.GetByName("Новое");
            
            addRequestDialog.ShowDialog();

            var ChEq = baseEquipmentRepository.GetByName(addRequestDialog.ChEquipmentSeries);
            var ChSer = baseServicesRepository.GetByName(addRequestDialog.ChEquipmentService);

            addRequestDialog.AddingRequest.cus = ChCus;
            addRequestDialog.AddingRequest.eq = ChEq;
            addRequestDialog.AddingRequest.ser = ChSer;
            addRequestDialog.AddingRequest.date_time_start = DateTime.Now;
            
            addRequestDialog.AddingRequest.recep = ChReception;
            addRequestDialog.AddingRequest.stat = ChStatus;

            baseRequestRepository.AddRequest(addRequestDialog.AddingRequest);

        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    => e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;


        private void find_customer_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void find_request_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox t = (TextBox)sender;
                string filter = t.Text;
                ICollectionView viewSource = CollectionViewSource.GetDefaultView(request_table.ItemsSource);
                if (filter == "") viewSource.Filter = null;
                else
                {
                    viewSource.Filter = o =>
                    {
                        ViewModelRequests p = o as ViewModelRequests;
                        return p.date_time_start.Date.ToString().Contains(filter);
                    };
                    request_table.ItemsSource = viewSource;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void customers_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chosenId = baseCustomersRepository.GetId(customers_table.SelectedIndex);

            //request_table.ItemsSource = baseRequestRepository.GetAllRequestsForCustomer(chosenId);
            var allRequests = new List<DB_Connections.Entities.Request>(baseRequestRepository.GetAllRequestsForCustomer(chosenId));

            List<ViewModelRequests> viewModelRequestsList = new List<ViewModelRequests>();
            //List<ViewModelRequests> viewModelRequestsList = Enumerable.Repeat(new ViewModelRequests(0, null, null, null, null, null, DateTime.Now, DateTime.Now), count).ToList();

            for (int row = 0; row < allRequests.Count(); row++)
            {
                string engineer;

                if (allRequests[row].eng == null)
                    engineer = null;
                else
                    engineer = allRequests[row].eng.name;

                viewModelRequestsList.Add(new ViewModelRequests(allRequests[row].id_req, allRequests[row].eq.series, allRequests[row].ser.name, allRequests[row].urgency, engineer, allRequests[row].stat.name,
                    allRequests[row].date_time_start, allRequests[row].date_time_end));
            }

            request_table.ItemsSource = viewModelRequestsList;
        }

        public class ViewModelRequests
        {
            [DisplayName("ID")]
            public int id { get; set; }

            [DisplayName("Оборудование (серия)")]
            public string equipment { get; set; }

            [DisplayName("Услуга")]
            public string service { get; set; }

            [DisplayName("Уровень срочности")]
            public string urgency { get; set; }

            [DisplayName("Имя инженера")]
            public string engineer { get; set; }

            [DisplayName("Статус")]
            public string status { get; set; }

            [DisplayName("Дата оформления")]
            public DateTime date_time_start { get; set; }

            [DisplayName("Дата завершения")]
            public DateTime? date_time_end { get; set; }

            public ViewModelRequests(int id, string equipment, string service, string urgency, string engineer, string status, DateTime date_time_start, DateTime? date_time_end)
            {
                this.id = id;
                this.equipment = equipment;
                this.service = service;
                this.urgency = urgency;
                this.engineer = engineer;
                this.status = status;
                this.date_time_start = date_time_start;
                this.date_time_end = date_time_end;
            }
        }
    }

}
