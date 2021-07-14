using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connections.Entities
{
    public class equipment
    {
        public int id_eq { get; }
        
        public string series { get; set; }

        public equipment_class eq_cl { get; set; }

        public vendor ven { get; set; }

        public equipment(int id_eq, string series, equipment_class eq_cl, vendor ven)
        {
            this.id_eq = id_eq;
            this.series = series;
            this.eq_cl = eq_cl;
            this.ven = ven;
        }

        public override string ToString()
        {
            return $"{id_eq}, {series}, {eq_cl}, {ven}";
        }
    }
}
