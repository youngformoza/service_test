using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    class MySQLStatusRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLStatusRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }


        public status GetByName(string name)
        {
            status ChStatus = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_stat, name FROM status WHERE name = @name", connection);

                command.Parameters.AddWithValue("name", name);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChStatus = new status(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChStatus;
        }

    }
}
