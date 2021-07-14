using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    class MySQLEmployeesRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLEmployeesRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }


        public employees GetByName(string name)
        {
            employees ChEmployee = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_empl, name, qualification, id_position, position.name FROM employees JOIN positions ON employees.id_position = positions.id_pos WHERE name = @name", connection);

                command.Parameters.AddWithValue("name", name);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChEmployee = new employees(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new positions(reader.GetInt32(3), reader.GetString(4)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChEmployee;
        }
    }
}
