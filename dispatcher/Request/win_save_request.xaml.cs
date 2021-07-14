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
        public DB_Connections.Entities.Request UpdatingRequest { get; set; }

        public string ChEquipmentSeries;
        public string ChEquipmentService;
        public string ChEquipmentStatus;

        public win_save_request()
        {
            InitializeComponent();
        }

        private void Apply_save_request(object sender, RoutedEventArgs e)
        {
            UpdatingRequest = new DB_Connections.Entities.Request(DateTime.Now, "0", null, null, null, null);

            UpdatingRequest.date_time_start = DateTime.Now;
            UpdatingRequest.urgency = urgency.Text;

            ChEquipmentSeries = equipment_series.Text;
            ChEquipmentService = service.Text;
            ChEquipmentStatus = status.Text;

            if (ChEquipmentStatus == "Завершен")
                UpdatingRequest.date_time_end = DateTime.Now;

            Close();
        }
    }
}