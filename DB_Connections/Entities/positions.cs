using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class positions
    {
        public int id_pos { get; }
        public string name { get; set; }

        public positions(int id_pos, string name)
        {
            this.id_pos = id_pos;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id_pos}, {name}";
        }
    }


}
