using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DB_Connections.Entities
{
    public class customer
    {
        [DisplayName("ID")]
        public int id_cus { get; }

        [DisplayName("ФИО")]
        public string name { get; set; }

        [DisplayName("Должность")]
        public string position_cus { get; set; }

        [DisplayName("День рождения")]
        public DateTime birthday { get; set; }

        [DisplayName("Почта")]
        public string mail { get; set; }

        [DisplayName("Номер телефона")]
        public long phone { get; set; }

        public customer(int id_cus, string name, string position, DateTime birthday, string mail, long phone)
        {
            this.id_cus = id_cus;
            this.name = name;
            this.position_cus = position;
            this.birthday = birthday;
            this.mail = mail;
            this.phone = phone;
        }

        public customer(string name, string position_cus, DateTime birthday, string mail, long phone)
        {
            this.name = name;
            this.position_cus = position_cus;
            this.birthday = birthday;
            this.mail = mail;
            this.phone = phone;
        }

        public customer(string name, string position_cus, string mail, long phone)
        {
            this.name = name;
            this.position_cus = position_cus;
            this.mail = mail;
            this.phone = phone;
        }

        public override string ToString()
        {
            return $"{id_cus}, {name}, {position_cus}, {birthday}, {mail}, {phone}";
        }
    }
}
