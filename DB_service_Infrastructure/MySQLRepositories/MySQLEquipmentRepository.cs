using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    public class MySQLEquipmentRepository : IBaseEquipmentRepository
    {
        protected string ConnectionString { get; set; }


        public MySQLEquipmentRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }


        public equipment GetByName(string name)
        {
            equipment ChEquipment = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_eq, series, id_class, equipment_class.`name`, id_vendor, vendor.`name` FROM equipment " +
                    "JOIN equipment_class ON equipment.id_class = equipment_class.id_eq_cl " +
                    "JOIN vendor ON equipment.id_vendor = vendor.id_ven " +
                    "WHERE series = @name", connection);

                command.Parameters.AddWithValue("name", name);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChEquipment = new equipment(reader.GetInt32(0), reader.GetString(1), 
                        new equipment_class(reader.GetInt32(2), reader.GetString(3)), 
                        new vendor(reader.GetInt32(4), reader.GetString(5)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChEquipment;
        }
    }
}
