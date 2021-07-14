using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class Request
    {
        public int id_req { get; }

        public DateTime date_time_start { get; set; }

        public DateTime date_time_end { get; set; }

        public string urgency { get; set; }

        public Customer cus { get; set; }

        public equipment eq { get; set; } 

        public services ser { get; set; }

        public employees recep { get; set; }

        public employees eng { get; set; }

        public status stat { get; set; }

        public Request(int id_req, DateTime date_time_start, DateTime date_time_end, string urgency, Customer cus, equipment eq, services ser, employees recep, employees eng, status stat)
        {
            this.id_req = id_req;
            this.date_time_start = date_time_start;
            this.date_time_end = date_time_end;
            this.urgency = urgency;
            this.cus = cus;
            this.eq = eq;
            this.ser = ser;
            this.recep = recep;
            this.eng = eng;
            this.stat = stat;
        }

        public Request(DateTime date_time_start, string urgency, Customer cus, equipment eq, services ser, employees recep, status stat)
        {
            this.date_time_start = date_time_start;
            this.urgency = urgency;
            this.cus = cus;
            this.eq = eq;
            this.ser = ser;
            this.recep = recep;
            this.stat = stat;
        }

        public Request(int id_req, DateTime date_time_start, string urgency, Customer cus, equipment eq, services ser, employees recep, status stat)
        {
            this.id_req = id_req;
            this.date_time_start = date_time_start;
            this.urgency = urgency;
            this.cus = cus;
            this.eq = eq;
            this.ser = ser;
            this.recep = recep;
            this.stat = stat;
        }

        public Request(DateTime date_time_start, string urgency, equipment eq, services ser, employees recep, status stat)
        {
            this.date_time_start = date_time_start;
            this.urgency = urgency;
            this.eq = eq;
            this.ser = ser;
            this.recep = recep;
            this.stat = stat;
        }

        public override string ToString()
        {
            return $"{id_req}, {date_time_start}, {date_time_end}, {urgency}, {cus}, {eq}, {ser}, {recep}, {eng}, {stat}";
        }
    }
}
