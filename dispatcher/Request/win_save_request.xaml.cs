using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using DB_service_Infrastructure.MySQLRepositories;
using service_center.repositories;

namespace dispatcher.Request
{
    /// <summary>
    /// Логика взаимодействия для win_save_request.xaml
    /// </summary> 

    public partial class win_save_request : Window
    {

        public DB_Connections.Entities.Request UpdatingRequest { get; set; }

        public string ChEquipmentService;
        public string ChEquipmentClass;
        public string ChEquipmentModel;
        public string ChEquipmentVendor;
        public string ChEquipmentStatus;
        public int exUpdFlag = 0;

        public IBaseEquipmentClassRepository baseEquipmentClassRepository = new MySQLEquipmentClassRepository();
        public IBaseEquipmentRepository baseEquipmentRepository = new MySQLEquipmentRepository();
        public IBaseServicesRepository baseServicesRepository = new MySQLServicesRepository();
        public IBaseStatusRepository baseStatusRepository = new MySQLStatusRepository();


        public win_save_request(DB_Connections.Entities.Request request)
        {
            UpdatingRequest = request;
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

            
            var statusList = new List<status>(baseStatusRepository.GetAllStatuses());
            List<ViewModelName> viewStatusList = new List<ViewModelName>();

            for (int row = 0; row < statusList.Count; row++)
            {
                viewStatusList.Add(new ViewModelName(statusList[row].name));
            }

            status.ItemsSource = viewStatusList;
            urgency.ItemsSource = new List<string> { "обычный", "высокий", "очень высокий" };

            equipment_table.ItemsSource = viewModelEquipmentList;

            equipment_class.Text = UpdatingRequest.eq.eq_cl.ToString();
            urgency.Text = UpdatingRequest.urgency;
            equipment_series.Text = UpdatingRequest.series;
            service.Text = UpdatingRequest.ser.name;
            status.Text = UpdatingRequest.stat.name;

            
            var allEquipmentForUpdating = new List<equipment>(baseEquipmentRepository.GetAllEquipmnetForClass(UpdatingRequest.eq.eq_cl.id_eq_cl));

            List<ViewModelEquipment> viewModelEquipmentListUpdating = new List<ViewModelEquipment>();

            for (int row = 0; row < allEquipmentForUpdating.Count; row++)
            {
                viewModelEquipmentListUpdating.Add(new ViewModelEquipment(allEquipmentForUpdating[row].ven.name, allEquipmentForUpdating[row].model, allEquipmentForUpdating[row].eq_cl.name));
            }

            equipment_table.ItemsSource = viewModelEquipmentListUpdating;

            int currentRow = -1;
            for (int row = 0; row < allEquipmentForUpdating.Count; row++)
            {
                if (viewModelEquipmentListUpdating[row].equipmentClass.ToString() == UpdatingRequest.eq.eq_cl.name.ToString() && viewModelEquipmentListUpdating[row].equipmentModel.ToString() == UpdatingRequest.eq.model.ToString()
                    && viewModelEquipmentListUpdating[row].equipmentVendor.ToString() == UpdatingRequest.eq.ven.name.ToString())
                    currentRow = row;
            }
            
            
            equipment_table.SelectedIndex = currentRow;

        }

        private void Apply_save_request(object sender, RoutedEventArgs e)
        {
            if (!((urgency.Text == "") || (equipment_series.Text == "") || (service.Text == "")))
            {
                if ((equipment_table.SelectedItem == null) || (equipment_class.SelectedItem == null))
                {
                    MessageBox.Show("Заполнены не все поля");
                    exUpdFlag = 1;
                }
                else
                {
                    UpdatingRequest.date_time_start = DateTime.Now;
                    UpdatingRequest.urgency = urgency.Text;

                    UpdatingRequest.series = equipment_series.Text;
                    var chEqClass = equipment_table.SelectedItem as ViewModelEquipment;
                    ChEquipmentClass = chEqClass.equipmentClass;
                    ChEquipmentModel = chEqClass.equipmentModel;
                    ChEquipmentVendor = chEqClass.equipmentVendor;
                    ChEquipmentService = service.Text;
                    ChEquipmentStatus = status.Text;

                    if (ChEquipmentStatus == "Завершен")
                        UpdatingRequest.date_time_end = DateTime.Now;
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля");
                exUpdFlag = 1;
            }
            Close();
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

            public override string ToString()
            {
                return $"{equipmentVendor}, {equipmentModel}, {equipmentClass}";
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