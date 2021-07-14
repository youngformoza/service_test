﻿using System;
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
    /// Логика взаимодействия для win_del_customer.xaml
    /// </summary>
    public partial class win_del_customer : Window
    {
        public static IBaseCustomersRepository baseCustomersRepository = new customers_repository();
        public win_del_customer()
        {
            InitializeComponent();
        }

        private void del_customer_Click(object sender, RoutedEventArgs e)
        {
            var custom_id = int.Parse(customer_id.Text);

            var customerToDel = baseCustomersRepository.GetById(custom_id);

            if (customerToDel == null)
            {
                Console.WriteLine("Произошла ошибка удаления");
                return;
            }

            baseCustomersRepository.Delete(customerToDel);

            Close();
        }
    }
}