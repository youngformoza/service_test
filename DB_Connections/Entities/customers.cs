using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class customers
    {
        public int id_cus { get; }
        public string name { get; set; }
        public string position_cus { get; set; }
        public DateTime birthday { get; set; }
        
        public string mail { get; set; }

        public long phone { get; set; }

        public customers(int id_cus, string name, string position, DateTime birthday, string mail, long phone)
        {
            this.id_cus = id_cus;
            this.name = name;
            this.position_cus = position;
            this.birthday = birthday;
            this.mail = mail;
            this.phone = phone;
        }

        public customers(string name, string position_cus, DateTime birthday, string mail, long phone)
        {
            this.name = name;
            this.position_cus = position_cus;
            this.birthday = birthday;
            this.mail = mail;
            this.phone = phone;
        }

        public override string ToString()
        {
            return $"{id_cus}, {name}, {position_cus}, {birthday}, {mail}, {phone}";
        }
    }
}
