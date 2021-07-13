using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class equipment_class
    {
        public int id_eq_cl { get; }
        public string name { get; set; }

        public equipment_class(int id_eq_cl, string name)
        {
            this.id_eq_cl = id_eq_cl;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id_eq_cl}, {name}";
        }
    }
}
