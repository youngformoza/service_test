using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using dispatcher.Customers;
using dispatcher.Request;
using DB_service_Infrastructure.MySQLRepositories;

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
            customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
        }



        private void save_customer(object sender, RoutedEventArgs e)
        {

            if (customers_table.SelectedItem != null)
            {
                var chosenCustomer = customers_table.SelectedItem as Customer;

                var updateCustomerDialog = new win_save_customer(baseCustomersRepository.GetById(chosenCustomer.id_cus));

                updateCustomerDialog.ShowDialog();

                if (updateCustomerDialog.exUpdFlag == 0)
                {
                    baseCustomersRepository.Update(updateCustomerDialog.UpdatingCustomer);

                    customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
                }
            }
            else
            {
                MessageBox.Show("Не выбран клиент!");
                return;
            }
        }

        private void add_customer(object sender, RoutedEventArgs e)
        {
            var addCustomerDialog = new win_add_customer(); 

            addCustomerDialog.ShowDialog();

            if (addCustomerDialog.exAddFlag == 0)
            {
                baseCustomersRepository.AddCustomer(addCustomerDialog.AddingCustomer);

                customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
            }

        }

        private void del_customer(object sender, RoutedEventArgs e)
        {
            if (customers_table.SelectedItem != null)
            {
                var chosenCustomer = customers_table.SelectedItem as Customer;
                var ChCus = baseCustomersRepository.GetById(chosenCustomer.id_cus);
                baseCustomersRepository.Delete(ChCus);
                customers_table.ItemsSource = baseCustomersRepository.GetCustomers();
            }
            else
            {
                MessageBox.Show("Не выбран клиент!");
                return;
            }

        }
        
        private void save_request(object sender, RoutedEventArgs e)
        {
            if (customers_table.SelectedItem != null)
            {
                if (request_table.SelectedItem != null)
                {
                    var chosenRequest = request_table.SelectedItem as ViewModelRequests;
                    var ChRequest = baseRequestRepository.GetById(chosenRequest.id);

                    var updRequestDialog = new win_save_request(ChRequest);
                    updRequestDialog.ShowDialog();

                    if (updRequestDialog.exUpdFlag == 0)
                    {
                        var ChEq = baseEquipmentRepository.GetByName(updRequestDialog.ChEquipmentModel);
                        var ChSer = baseServicesRepository.GetByName(updRequestDialog.ChEquipmentService);
                        var chosenCustomer = customers_table.SelectedItem as Customer;
                        var ChCus = baseCustomersRepository.GetById(chosenCustomer.id_cus);
                        var ChStat = baseStatusRepository.GetByName(updRequestDialog.ChEquipmentStatus);

                        updRequestDialog.UpdatingRequest.cus = ChCus;
                        updRequestDialog.UpdatingRequest.eq = ChEq;
                        updRequestDialog.UpdatingRequest.ser = ChSer;
                        updRequestDialog.UpdatingRequest.stat = ChStat;

                        baseRequestRepository.Update(updRequestDialog.UpdatingRequest);

                        showInRequestTable();
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана заявка!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не выбран клиент!");
                return;
            }
        }

        private void add_request(object sender, RoutedEventArgs e)
        {
            var addRequestDialog = new win_add_request();
            
            if(customers_table.SelectedItem == null)
            {
                MessageBox.Show("Не выбран клиент!");
                return;
            }
            else
            {
                var chosenCustomer = customers_table.SelectedItem as Customer;
                var ChCus = baseCustomersRepository.GetById(chosenCustomer.id_cus);
                var ChReception = baseEmployeesRepository.GetById(1);
                var ChStatus = baseStatusRepository.GetByName("Новое");
            
                addRequestDialog.ShowDialog();


                if (addRequestDialog.exAddFlag == 0)
                {
                    var ChEq = baseEquipmentRepository.GetByName(addRequestDialog.ChEquipmentModel);
                    var ChSer = baseServicesRepository.GetByName(addRequestDialog.ChEquipmentService);

                    addRequestDialog.AddingRequest.cus = ChCus;
                    addRequestDialog.AddingRequest.eq = ChEq;
                    addRequestDialog.AddingRequest.ser = ChSer;
                    addRequestDialog.AddingRequest.date_time_start = DateTime.Now;
                     

                    addRequestDialog.AddingRequest.recep = ChReception;
                    addRequestDialog.AddingRequest.stat = ChStatus;

                    baseRequestRepository.AddRequest(addRequestDialog.AddingRequest);
                }

                showInRequestTable();
            }
                        

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
            if (customers_table.SelectedItem != null)
            {

                showInRequestTable();
            }
        }

        public void showInRequestTable()
        {
            var chosenCustomer = customers_table.SelectedItem as Customer;
            var ChCus = baseCustomersRepository.GetById(chosenCustomer.id_cus);

            var allRequests = new List<DB_Connections.Entities.Request>(baseRequestRepository.GetAllRequestsForCustomer(ChCus.id_cus));

            List<ViewModelRequests> viewModelRequestsList = new List<ViewModelRequests>();

            for (int row = 0; row < allRequests.Count(); row++)
            {
                string engineer;

                if (allRequests[row].eng == null)
                    engineer = null;
                else
                    engineer = allRequests[row].eng.name;

                viewModelRequestsList.Add(new ViewModelRequests(allRequests[row].id_req, allRequests[row].eq.ven.name, allRequests[row].eq.model, allRequests[row].series,
                    allRequests[row].ser.name, allRequests[row].urgency, engineer, allRequests[row].stat.name, allRequests[row].date_time_start, allRequests[row].date_time_end));
            }

            request_table.ItemsSource = viewModelRequestsList;
        }

        public class ViewModelRequests
        {
            [DisplayName("ID")]
            public int id { get; set; }

            [DisplayName("Оборудование (производитель)")]
            public string equipmentVendor { get; set; }

            [DisplayName("Оборудование (модель)")]
            public string equipmentModel { get; set; }

            [DisplayName("Оборудование (серия)")]
            public string equipmentSeries { get; set; }

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

            public ViewModelRequests(int id, string equipmentVendor, string equipmentModel, string equipmentSeries, string service, string urgency, string engineer, string status, DateTime date_time_start, DateTime? date_time_end)
            {
                this.id = id;
                this.equipmentVendor = equipmentVendor;
                this.equipmentModel = equipmentModel;
                this.equipmentSeries = equipmentSeries;
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
