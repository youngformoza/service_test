using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    class MySQLPositionsRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLPositionsRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }
        

        public positions GetById(int id)
        {
            positions ChPosition = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id, name FROM positions WHERE id_pos = @id", connection);

                command.Parameters.AddWithValue("id", id);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChPosition = new positions(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChPosition;
        }
                                
    }
}
