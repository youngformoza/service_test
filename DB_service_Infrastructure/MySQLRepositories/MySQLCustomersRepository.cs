using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    public class MySQLCustomersRepository : IBaseCustomersRepository 
    {
        protected string ConnectionString { get; set; }

        public MySQLCustomersRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }

        public Customer[] GetCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_cus, `name`, `position`, birthday, mail, phone FROM customers; ", connection);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetInt64(5)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return customers.ToArray();
        }


        public Customer GetById(int id)
        {
            Customer ChCustomer = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_cus, `name`, `position`, birthday, mail, phone FROM customers WHERE id_cus = @id", connection);

                command.Parameters.AddWithValue("id", id);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChCustomer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetInt64(5));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChCustomer;
        }

        public void AddCustomer(Customer NewCustomer)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("INSERT INTO customers (`name`, `position`, birthday, mail, phone) VALUES " +
                    "(@name, @position, @birthday, @mail, @phone)", connection);


                command.Parameters.AddWithValue("name", NewCustomer.name);
                command.Parameters.AddWithValue("position", NewCustomer.position_cus);
                command.Parameters.AddWithValue("birthday", NewCustomer.birthday);
                command.Parameters.AddWithValue("mail", NewCustomer.mail);
                command.Parameters.AddWithValue("phone", NewCustomer.phone);

                using var writer = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //return NewCustomer;
        }

        public void Update(Customer UpdatingCustomer)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("UPDATE customers SET `name` = @name, position = @position, birthday = @birthday, " +
                    "mail = @mail, phone = @phone WHERE id_cus = @id;", connection);

                command.Parameters.AddWithValue("id", UpdatingCustomer.id_cus);
                command.Parameters.AddWithValue("name", UpdatingCustomer.name);
                command.Parameters.AddWithValue("position", UpdatingCustomer.position_cus);
                command.Parameters.AddWithValue("birthday", UpdatingCustomer.birthday);
                command.Parameters.AddWithValue("mail", UpdatingCustomer.mail);
                command.Parameters.AddWithValue("phone", UpdatingCustomer.phone);

                using var writer = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //return UpdatingCustomer;
        }

        public void Delete(Customer DeletingCustomer)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("DELETE FROM customers WHERE id_cus = @id", connection);

                command.Parameters.AddWithValue("id", DeletingCustomer.id_cus);

                using var reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
