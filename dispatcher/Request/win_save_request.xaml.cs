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
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using service_center.repositories;

namespace dispatcher.Request
{
    /// <summary>
    /// Логика взаимодействия для win_save_request.xaml
    /// </summary>
    public partial class win_save_request : Window
    {
        public static IBaseCustomersRepository baseCustomersRepository = new CustomersRepository();
        public static IBaseRequestsRepository baseRequestRepository = new RequestsRepository();

        public static IBaseVendorRepository baseVendorRepository = new vendor_repository();
        public static IBaseStatusRepository baseStatusRepository = new status_repository();
        public static IBaseServicesRepository baseServicesRepository = new services_repository();
        public static IBasePositionRepository basePositionRepository = new positions_repository();
        public static IBaseEquipmentClassRepository baseEquipmentClassRepository = new equipment_class_repository();
        public static IBaseEquipmentRepository baseEquipmentRepository = new equipment_repository();
        public static IBaseEmployeesRepository baseEmployeesRepository = new employees_repository();

        public win_save_request()
        {
            InitializeComponent();
        }

        private void Apply_save_request(object sender, RoutedEventArgs e)
        {
            var ch_equipment = baseEquipmentRepository.GetByName(equipment_series.Text);
            var ch_service = baseServicesRepository.GetByName(service.Text);
            var empl_recep = baseEmployeesRepository.GetById(1); ///////////////////////// ПЕРЕДАВАТЬ В ФУНКЦИЮ ID ДИСПЕТЧЕРА
            var ch_status = baseStatusRepository.GetByName(status.Text);
            baseRequestRepository.Update(new request(DateTime.Now, urgency.Text, ch_equipment, ch_service, empl_recep, ch_status));
        }
    }
}
