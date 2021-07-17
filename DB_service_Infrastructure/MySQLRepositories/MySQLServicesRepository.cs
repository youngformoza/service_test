using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    public class MySQLServicesRepository : IBaseServicesRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLServicesRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }


        public services GetByName(string name)
        {
            services ChService = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_ser, name FROM services WHERE name = @name", connection);

                command.Parameters.AddWithValue("name", name);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChService = new services(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChService;
        }

        public services[] GetAllServices()
        {
            var servicesList = new List<services>();

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_ser, name FROM services; ", connection);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servicesList.Add(new services(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return servicesList.ToArray();
        }
    }
}
