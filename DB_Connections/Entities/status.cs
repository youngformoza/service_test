using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class status
    {
        public int id_stat { get; }
        public string name { get; set; }

        public status(int id_stat, string name)
        {
            this.id_stat = id_stat;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id_stat}, {name}";
        }
    }
}
