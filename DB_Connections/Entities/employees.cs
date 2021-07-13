using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class employees
    {
        public int id_empl { get; }
        
        public string name { get; set; }
        
        public string quilification { get; set; }

        public positions position_empl { get; set; }

        public employees(int id_empl, string name, string quilification, positions position_empl)
        {
            this.id_empl = id_empl;
            this.name = name;
            this.quilification = quilification;
            this.position_empl = position_empl;
        }

        public override string ToString()
        {
            return $"{id_empl}, {name}, {quilification}, {position_empl}";
        }
    }
}
