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


namespace dispatcher.Customers
{
    /// <summary>
    /// Логика взаимодействия для win_add_customer.xaml
    /// </summary>
    public partial class win_add_customer : Window
    {
        public static IBaseCustomersRepository baseCustomersRepository = new customers_repository();
        public win_add_customer()
        {
            InitializeComponent();
        }

        private void Apply_add_customer(object sender, RoutedEventArgs e)
        {
            baseCustomersRepository.AddCustomer(new customer(customer_name.Text, customer_position.Text, bd_customer.DisplayDate, customer_mail.Text, int.Parse(customer_phone.Text)));
            Close();
        }
    }
}
