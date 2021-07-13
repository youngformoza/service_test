using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class vendor
    {
        public int id_ven { get; }
        public string name { get; set; }

        public vendor(int id_ven, string name)
        {
            this.id_ven = id_ven;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id_ven}, {name}";
        }
    }
}
