using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;
using MySql.Data.MySqlClient;

namespace DB_service_Infrastructure.MySQLRepositories
{
    public class MySQLRequestsRepository : IBaseRequestsRepository
    {

        protected string ConnectionString { get; set; }


        public MySQLRequestsRepository()
        {
            ConnectionString = "server=localhost;user=root;database=service_center;port=3306;password=2907Yu1909Ya";
        }

        public Request[] GetRequests()
        {
            var allRequests = new List<Request>();

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_req, date_time_start, date_time_end, urgency, series," +
                    "id_customer, customers.`name`, customers.`position`, customers.birthday, customers.mail, customers.phone, " +
                    "id_equipment, equipment.model, equipment.id_class, equipment_class.`name`, id_vendor, vendor.`name`, " +
                    "id_service, services.`name`, " +
                    "id_employee_reception, reception.`name`, reception.qualification, reception.id_position, reception_position.`name`, " +
                    "id_employee_engineer, engineer.`name`, engineer.qualification, engineer.id_position, engineer_position.`name`, " +
                    "id_status, `status`.`name` " +
                    "FROM requests " +
                    "JOIN customers ON requests.id_customer = customers.id_cus " +
                    "JOIN equipment ON requests.id_equipment = equipment.id_eq " +
                    "JOIN equipment_class ON equipment.id_class = equipment_class.id_eq_cl " +
                    "JOIN vendor ON equipment.id_vendor = vendor.id_ven " +
                    "JOIN services ON requests.id_service = services.id_ser " +
                    "JOIN employees AS reception ON requests.id_employee_reception = reception.id_empl " +
                    "JOIN `position` AS reception_position ON reception.id_position = reception_position.id_pos " +
                    "LEFT JOIN employees AS engineer ON requests.id_employee_engineer = engineer.id_empl " +
                    "LEFT JOIN `position` AS engineer_position ON engineer.id_position = engineer_position.id_pos " +
                    "JOIN `status` ON requests.id_status = `status`.id_stat;", connection);

                using var reader = command.ExecuteReader();

                DateTime? date_time_end;
                employees engineer;

                while (reader.Read())
                {
                    if (reader.IsDBNull(reader.GetOrdinal("date_time_end")))
                        date_time_end = null;
                    else
                        date_time_end = reader.GetDateTime(2);

                    if (reader.IsDBNull(reader.GetOrdinal("id_employee_engineer")))
                        engineer = null;
                    else
                        engineer = new employees(reader.GetInt32(24), reader.GetString(25), reader.GetString(26), new positions(reader.GetInt32(27), reader.GetString(28)));

                    allRequests.Add(new Request(reader.GetInt32(0), reader.GetDateTime(1), date_time_end, reader.GetString(3), reader.GetString(4),
                        new Customer(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetString(9), reader.GetInt64(10)),
                        new equipment(reader.GetInt32(11), reader.GetString(12), new equipment_class(reader.GetInt32(13), reader.GetString(14)), new vendor(reader.GetInt32(15), reader.GetString(16))),
                        new services(reader.GetInt32(17), reader.GetString(18)),
                        new employees(reader.GetInt32(19), reader.GetString(20), reader.GetString(21), new positions(reader.GetInt32(22), reader.GetString(23))),
                        engineer,
                        new status(reader.GetInt32(29), reader.GetString(30))));
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return allRequests.ToArray();
        }

        public Request[] GetAllRequestsForCustomer(int idCustomer)
        {
            var allRequests = new List<Request>();

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_req, date_time_start, date_time_end, urgency, series," +
                    "id_customer, customers.`name`, customers.`position`, customers.birthday, customers.mail, customers.phone," +
                    "id_equipment, equipment.model, equipment.id_class, equipment_class.`name`, id_vendor, vendor.`name`," +
                    "id_service, services.`name`," +
                    "id_employee_reception, reception.`name`, reception.qualification, reception.id_position, reception_position.`name`," +
                    "id_employee_engineer, engineer.`name`, engineer.qualification, engineer.id_position, engineer_position.`name`," +
                    "id_status, `status`.`name` " +
                    "FROM requests " + 
                    "JOIN customers ON requests.id_customer = customers.id_cus " +
                    "JOIN equipment ON requests.id_equipment = equipment.id_eq " +
                    "JOIN equipment_class ON equipment.id_class = equipment_class.id_eq_cl " +
                    "JOIN vendor ON equipment.id_vendor = vendor.id_ven " +
                    "JOIN services ON requests.id_service = services.id_ser " +
                    "JOIN employees AS reception ON requests.id_employee_reception = reception.id_empl " +
                    "JOIN `position` AS reception_position ON reception.id_position = reception_position.id_pos " +
                    "LEFT JOIN employees AS engineer ON requests.id_employee_engineer = engineer.id_empl " +
                    "LEFT JOIN `position` AS engineer_position ON engineer.id_position = engineer_position.id_pos " +
                    "JOIN `status` ON requests.id_status = `status`.id_stat " +
                    "WHERE id_customer = @id;", connection);

                command.Parameters.AddWithValue("id", idCustomer);

                using var reader = command.ExecuteReader();

                DateTime? date_time_end;
                employees engineer;

                while (reader.Read())
                {
                    if (reader.IsDBNull(reader.GetOrdinal("date_time_end")))
                        date_time_end = null;
                    else
                        date_time_end = reader.GetDateTime(2);

                    if (reader.IsDBNull(reader.GetOrdinal("id_employee_engineer")))
                        engineer = null;
                    else
                        engineer = new employees(reader.GetInt32(24), reader.GetString(25), reader.GetString(26), new positions(reader.GetInt32(27), reader.GetString(28)));

                    allRequests.Add(new Request(reader.GetInt32(0), reader.GetDateTime(1), date_time_end, reader.GetString(3), reader.GetString(4),
                        new Customer(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetString(9), reader.GetInt64(10)),
                        new equipment(reader.GetInt32(11), reader.GetString(12), new equipment_class(reader.GetInt32(13), reader.GetString(14)), new vendor(reader.GetInt32(15), reader.GetString(16))),
                        new services(reader.GetInt32(17), reader.GetString(18)),
                        new employees(reader.GetInt32(19), reader.GetString(20), reader.GetString(21), new positions(reader.GetInt32(22), reader.GetString(23))),
                        engineer,
                        new status(reader.GetInt32(29), reader.GetString(30))));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return allRequests.ToArray();
        }

        public Request GetById(int id)
        {
            Request ChRequest = null;

            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("SELECT id_req, date_time_start, date_time_end, urgency, series, " +
                              "id_customer, customers.`name`, customers.`position`, customers.birthday, customers.mail, customers.phone," +
                              "id_equipment, equipment.model, equipment.id_class, equipment_class.`name`, id_vendor, vendor.`name`," +
                              "id_service, services.`name`," +
                              "id_employee_reception, reception.`name`, reception.qualification, reception.id_position, reception_position.`name`," +
                              "id_employee_engineer, engineer.`name`, engineer.qualification, engineer.id_position, engineer_position.`name`," +
                              "id_status, `status`.`name` " +
                              "FROM requests " +
                              "JOIN customers ON requests.id_customer = customers.id_cus " +
                              "JOIN equipment ON requests.id_equipment = equipment.id_eq " +
                              "JOIN equipment_class ON equipment.id_class = equipment_class.id_eq_cl " +
                              "JOIN vendor ON equipment.id_vendor = vendor.id_ven " +
                              "JOIN services ON requests.id_service = services.id_ser " +
                              "JOIN employees AS reception ON requests.id_employee_reception = reception.id_empl " +
                              "JOIN `position` AS reception_position ON reception.id_position = reception_position.id_pos " +
                              "LEFT JOIN employees AS engineer ON requests.id_employee_engineer = engineer.id_empl " +
                              "LEFT JOIN `position` AS engineer_position ON engineer.id_position = engineer_position.id_pos " +
                              "JOIN `status` ON requests.id_status = `status`.id_stat "+
                              "WHERE id_req = @id", connection);

                command.Parameters.AddWithValue("id", id);

                using var reader = command.ExecuteReader();

                DateTime? date_time_end;
                employees engineer;

                while (reader.Read())
                {
                    if (reader.IsDBNull(reader.GetOrdinal("date_time_end")))
                        date_time_end = null;
                    else
                        date_time_end = reader.GetDateTime(2);

                    if (reader.IsDBNull(reader.GetOrdinal("id_employee_engineer")))
                        engineer = null;
                    else
                        engineer = new employees(reader.GetInt32(24), reader.GetString(25), reader.GetString(26), new positions(reader.GetInt32(27), reader.GetString(28)));

                    ChRequest = new Request(reader.GetInt32(0), reader.GetDateTime(1), date_time_end, reader.GetString(3), reader.GetString(4),
                        new Customer(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetString(9), reader.GetInt64(10)),
                        new equipment(reader.GetInt32(11), reader.GetString(12), new equipment_class(reader.GetInt32(13), reader.GetString(14)), new vendor(reader.GetInt32(15), reader.GetString(16))),
                        new services(reader.GetInt32(17), reader.GetString(18)),
                        new employees(reader.GetInt32(19), reader.GetString(20), reader.GetString(21), new positions(reader.GetInt32(22), reader.GetString(23))),
                        engineer,
                        new status(reader.GetInt32(29), reader.GetString(30)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ChRequest;
        }

        public void AddRequest(Request NewRequest)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("INSERT INTO requests (date_time_start, urgency, series, id_customer, id_equipment, id_service, id_employee_reception, id_status) VALUES " +
                    "(@date_time_start, @urgency, @series, @id_customer, @id_equipment, @id_service, @id_employee_reception, @id_status)", connection);

                command.Parameters.AddWithValue("date_time_start", NewRequest.date_time_start);
                command.Parameters.AddWithValue("urgency", NewRequest.urgency);
                command.Parameters.AddWithValue("series", NewRequest.series);
                command.Parameters.AddWithValue("id_customer", NewRequest.cus.id_cus);
                command.Parameters.AddWithValue("id_equipment", NewRequest.eq.id_eq);
                command.Parameters.AddWithValue("id_service", NewRequest.ser.id_ser);
                command.Parameters.AddWithValue("id_employee_reception", NewRequest.recep.id_empl);
                command.Parameters.AddWithValue("id_status", NewRequest.stat.id_stat);

                using var writer = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           // return NewRequest;
        }

        public void Update(Request UpdatingRequest)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();
                

                if (UpdatingRequest.stat.name == "Завершено" )
                {
                    using var command = new MySqlCommand("UPDATE requests SET date_time_start = @date_time_start, date_time_end = @date_time_end, urgency = @urgency, series = @series, id_equipment = @id_equipment, id_service = @id_service, " +
                        "id_status = @id_status WHERE id_req = @id;", connection);

                    command.Parameters.AddWithValue("id", UpdatingRequest.id_req);
                    command.Parameters.AddWithValue("date_time_start", UpdatingRequest.date_time_start);
                    command.Parameters.AddWithValue("date_time_end", DateTime.Now);
                    command.Parameters.AddWithValue("urgency", UpdatingRequest.urgency);
                    command.Parameters.AddWithValue("series", UpdatingRequest.series);
                    command.Parameters.AddWithValue("id_equipment", UpdatingRequest.eq.id_eq);
                    command.Parameters.AddWithValue("id_service", UpdatingRequest.ser.id_ser);
                    command.Parameters.AddWithValue("id_status", UpdatingRequest.stat.id_stat);

                    using var writer = command.ExecuteReader();
                }
                else
                {
                    using var command = new MySqlCommand("UPDATE requests SET date_time_start = @date_time_start, urgency = @urgency, series = @series, id_equipment = @id_equipment, id_service = @id_service, " +
                        "id_status = @id_status WHERE id_req = @id;", connection);

                    command.Parameters.AddWithValue("id", UpdatingRequest.id_req);
                    command.Parameters.AddWithValue("date_time_start", UpdatingRequest.date_time_start);
                    command.Parameters.AddWithValue("urgency", UpdatingRequest.urgency);
                    command.Parameters.AddWithValue("series", UpdatingRequest.series);
                    command.Parameters.AddWithValue("id_equipment", UpdatingRequest.eq.id_eq);
                    command.Parameters.AddWithValue("id_service", UpdatingRequest.ser.id_ser);
                    command.Parameters.AddWithValue("id_status", UpdatingRequest.stat.id_stat);

                    using var writer = command.ExecuteReader();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

          //  return UpdatingRequest;
        }

        public void Delete(Request DeletingRequest)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);

                connection.Open();

                using var command = new MySqlCommand("DELETE FROM requests WHERE id_req = @id", connection);

                command.Parameters.AddWithValue("id", DeletingRequest.id_req);

                using var reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
