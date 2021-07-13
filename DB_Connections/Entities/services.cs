using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class services
    {
        public int id_ser { get; }
        public string name { get; set; }

        public services(int id_ser, string name)
        {
            this.id_ser = id_ser;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id_ser}, {name}";
        }
    }
}
