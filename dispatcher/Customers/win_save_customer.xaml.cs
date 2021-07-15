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
    /// Логика взаимодействия для win_save_customer.xaml
    /// </summary>
    public partial class win_save_customer : Window
    {
        public Customer UpdatingCustomer { get; set; }

        public win_save_customer(Customer customer)
        {
            UpdatingCustomer = customer;

            InitializeComponent();

            customer_name.Text = UpdatingCustomer?.name;
            customer_position.Text = UpdatingCustomer?.position_cus;
            customer_mail.Text = UpdatingCustomer?.mail;
            customer_phone.Text = UpdatingCustomer?.phone.ToString();
            bd_customer.SelectedDate = UpdatingCustomer?.birthday;
        }

        private void Apply_save_customer(object sender, RoutedEventArgs e)
        {
            UpdatingCustomer.name = customer_name.Text;
            UpdatingCustomer.position_cus = customer_position.Text;
            UpdatingCustomer.mail = customer_mail.Text;

            if (int.TryParse(customer_phone.Text, out var number))
                   UpdatingCustomer.phone = number;

            UpdatingCustomer.birthday = bd_customer.SelectedDate.Value;

            Close();
        }
    }
}
