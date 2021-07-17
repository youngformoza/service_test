using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    public class MySQLEquipmentClassRepository : IBaseEquipmentClassRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLEquipmentClassRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }


        public equipment_class GetByName(string name)
        {
            equipment_class ChEqCl = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_eq_cl, name FROM equipment_class " +
                    "WHERE name = @name", connection);

                command.Parameters.AddWithValue("name", name);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChEqCl = new equipment_class(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChEqCl;
        }

        public equipment_class[] GetAllClasses()
        {
            var classes = new List<equipment_class>();

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_eq_cl, name FROM equipment_class; ", connection);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classes.Add(new equipment_class(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return classes.ToArray();
        }
    }
}
