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
        public Customer AddingCustomer { get; set; }

        
        public win_add_customer()
        {
            InitializeComponent();

        }

        private void Apply_add_customer(object sender, RoutedEventArgs e)
        {

            // Customer AddingCustomer = new Customer(customer_name.Text, customer_position.Text, bd_customer.SelectedDate.Value, customer_mail.Text, int.Parse(customer_phone.Text));

            AddingCustomer = new Customer("0", "0", "0", '0');

            AddingCustomer.name = customer_name.Text;
            AddingCustomer.position_cus = customer_position.Text;
            AddingCustomer.mail = customer_mail.Text;

            if (int.TryParse(customer_phone.Text, out var number))
                AddingCustomer.phone = number;
            AddingCustomer.birthday = bd_customer.SelectedDate.Value;
            
            Close();
        }

    }
}
