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
using service_center.repositories;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using service_center.repositories;

namespace dispatcher.Request
{
    /// <summary>
    /// Логика взаимодействия для win_add_request.xaml
    /// </summary>
    public partial class win_add_request : Window
    {
        public static IBaseCustomersRepository baseCustomersRepository = new customers_repository();
        public static IBaseRequestsRepository baseRequestRepository = new requests_repository();

        public static IBaseVendorRepository baseVendorRepository = new vendor_repository();
        public static IBaseStatusRepository baseStatusRepository = new status_repository();
        public static IBaseServicesRepository baseServicesRepository = new services_repository();
        public static IBasePositionRepository basePositionRepository = new positions_repository(); 
        public static IBaseEquipmentClassRepository baseEquipmentClassRepository = new equipment_class_repository();
        public static IBaseEquipmentRepository baseEquipmentRepository = new equipment_repository();
        public static IBaseEmployeesRepository baseEmployeesRepository = new employees_repository();



        public win_add_request()
        {
            InitializeComponent();
        }

        private void Apply_add_request(object sender, RoutedEventArgs e)
        {
            var ch_equipment = baseEquipmentRepository.GetByName(equipment_series.Text);
            var ch_service = baseServicesRepository.GetByName(service.Text);
            var customer = baseCustomersRepository.GetById(1); //////////////////////////// ИЗМЕНИТЬ НА ВЫБРАННЫЙ В ДРУГОЙ ТАБЛИЦЕ *передавать в функцию значение*
            var empl_recep = baseEmployeesRepository.GetById(1); ///////////////////////// ПЕРЕДАВАТЬ В ФУНКЦИЮ ID ДИСПЕТЧЕРА
            var ch_status = baseStatusRepository.GetByName("Добавлен");
            baseRequestRepository.AddRequest(new request(DateTime.Now, urgency.Text, customer, ch_equipment, ch_service, empl_recep, ch_status));

            Close();
        }
    }
}
