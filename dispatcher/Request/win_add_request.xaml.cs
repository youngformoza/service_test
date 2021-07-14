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
        public DB_Connections.Entities.Request AddingRequest { get; set; }

        public string ChEquipmentSeries;
        public string ChEquipmentService;


        public win_add_request()
        {
            InitializeComponent();
        }

        private void Apply_add_request(object sender, RoutedEventArgs e)
        {

            AddingRequest = new DB_Connections.Entities.Request(DateTime.Now, "0", null, null, null, null);

            AddingRequest.date_time_start = DateTime.Now;
            AddingRequest.urgency = urgency.Text;

            ChEquipmentSeries = equipment_series.Text;
            ChEquipmentService = service.Text;

            Close();
            
        }
    }
}