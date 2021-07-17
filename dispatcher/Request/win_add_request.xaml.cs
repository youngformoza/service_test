using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using DB_service_Infrastructure.MySQLRepositories;

namespace dispatcher.Request
{
    /// <summary>
    /// Логика взаимодействия для win_add_request.xaml
    /// </summary>
    public partial class win_add_request : Window
    {
        public DB_Connections.Entities.Request AddingRequest { get; set; }

        public string ChEquipmentService;
        public string ChEquipmentClass;
        public string ChEquipmentModel;
        public string ChEquipmentVendor;
        public int exAddFlag = 0;


        public IBaseEquipmentClassRepository baseEquipmentClassRepository = new MySQLEquipmentClassRepository();
        public IBaseEquipmentRepository baseEquipmentRepository = new MySQLEquipmentRepository();
        public IBaseServicesRepository baseServicesRepository = new MySQLServicesRepository();
        
        private static readonly List<ViewModelName> urgencyList = new List<ViewModelName>();

        public win_add_request()
        {
            InitializeComponent();
            equipment_class.ItemsSource = baseEquipmentClassRepository.GetAllClasses();

            var serviceList = new List<services>(baseServicesRepository.GetAllServices());
            List<ViewModelName> viewServiceList = new List<ViewModelName>();

            for (int row = 0; row < serviceList.Count; row++)
            {
                viewServiceList.Add(new ViewModelName(serviceList[row].name));
            }

            service.ItemsSource = viewServiceList;

            var allEquipment = new List<equipment>(baseEquipmentRepository.GetAllEquipment());

            List<ViewModelEquipment> viewModelEquipmentList = new List<ViewModelEquipment>();

            for (int row = 0; row < allEquipment.Count; row++)
            {
                viewModelEquipmentList.Add(new ViewModelEquipment(allEquipment[row].ven.name, allEquipment[row].model, allEquipment[row].eq_cl.name));
            }

            equipment_table.ItemsSource = viewModelEquipmentList;

            urgency.ItemsSource = new List<string> { "обычный", "высокий", "очень высокий" };
        }
        


        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    => e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        public class ViewModelEquipment
        {
            [DisplayName("Производитель")]
            public string equipmentVendor { get; set; }

            [DisplayName("Модель")]
            public string equipmentModel { get; set; }

            [DisplayName("Класс")]
            public string equipmentClass { get; set; }

            public ViewModelEquipment(string equipmentVendor, string equipmentModel, string equipmentClass)
            {
                this.equipmentVendor = equipmentVendor;
                this.equipmentModel = equipmentModel;
                this.equipmentClass = equipmentClass;
            }
        }

        public class ViewModelName
        {
            public string name { get; set; }

            public ViewModelName(string name)
            {
                this.name = name;
            }
            
            public override string ToString()
            {
                return $"{name}";
            }
        }


       
        private void find_equipment_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox t = (TextBox)sender;
                string filter = t.Text;
                ICollectionView viewSource = CollectionViewSource.GetDefaultView(equipment_table.ItemsSource);
                if (filter == "") viewSource.Filter = null;
                else
                {
                    viewSource.Filter = o =>
                    {
                        ViewModelEquipment p = o as ViewModelEquipment;
                        return (p.equipmentModel.ToString().Contains(filter) || p.equipmentVendor.ToString().Contains(filter));
                    };
                    equipment_table.ItemsSource = viewSource;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Apply_add_request(object sender, RoutedEventArgs e)
        {

            AddingRequest = new DB_Connections.Entities.Request(DateTime.Now, "0", null, null, null, null, null);

            if (!((urgency.Text == "") || (equipment_series.Text == "") || (service.Text == "")))
            {
                if ((equipment_table.SelectedItem == null) || (equipment_class.SelectedItem == null))
                {
                    MessageBox.Show("Заполнены не все поля");
                    exAddFlag = 1;
                }
                else
                {
                    AddingRequest.date_time_start = DateTime.Now;
                    AddingRequest.urgency = urgency.Text;

                    AddingRequest.series = equipment_series.Text;
                    var chEqClass = equipment_table.SelectedItem as ViewModelEquipment;
                    ChEquipmentClass = chEqClass.equipmentClass;
                    ChEquipmentModel = chEqClass.equipmentModel;
                    ChEquipmentVendor = chEqClass.equipmentVendor;
                    ChEquipmentService = service.Text;
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля");
                exAddFlag = 1;
            }
            Close();

        }

        private void equipment_class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chosenClass = equipment_class.SelectedItem as equipment_class;
            var ChClass = baseEquipmentClassRepository.GetByName(chosenClass.name);

            var allEquipment = new List<equipment>(baseEquipmentRepository.GetAllEquipmnetForClass(ChClass.id_eq_cl));

            List<ViewModelEquipment> viewModelEquipmentList = new List<ViewModelEquipment>();

            for (int row = 0; row < allEquipment.Count; row++)
            {
                viewModelEquipmentList.Add(new ViewModelEquipment(allEquipment[row].ven.name, allEquipment[row].model, allEquipment[row].eq_cl.name));
            }

            equipment_table.ItemsSource = viewModelEquipmentList;
        }

        
    }
}