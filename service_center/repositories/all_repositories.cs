using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;

namespace service_center.repositories
{
    

    public class vendor_repository : IBaseVendorRepository
    {
        public List<vendor> vendor_list = new List<vendor>();


        public vendor_repository()
        {
            vendor_list.Add(new vendor(1, "Al"));
            vendor_list.Add(new vendor(2, "Bl"));
            vendor_list.Add(new vendor(3, "Cl"));
            vendor_list.Add(new vendor(4, "Dl"));
            vendor_list.Add(new vendor(5, "El"));
        }
        public vendor GetByName(string name)
        {
            vendor ch_vendor = vendor_list.Find(item => item.name == name);

            return ch_vendor;
        }
    }
    public class status_repository : IBaseStatusRepository
    {
        public List<status> status_list = new List<status>();


        public status_repository()
        {
            status_list.Add(new status(1, "Al"));
            status_list.Add(new status(2, "Bl"));
            status_list.Add(new status(3, "Cl"));
            status_list.Add(new status(4, "Dl"));
            status_list.Add(new status(5, "El"));
        }

        public status[] GetAllStatuses()
        {
            return status_list.ToArray();
        }

        public status GetByName(string name)
        {
            status ch_status = status_list.Find(item => item.name == name);

            return ch_status;
        }
    }
    public class services_repository : IBaseServicesRepository
    {
        public List<services> services_list = new List<services>();
        
        public services_repository()
        {
            services_list.Add(new services(1, "A"));
            services_list.Add(new services(2, "B"));
            services_list.Add(new services(3, "C"));
            services_list.Add(new services(4, "D"));
            services_list.Add(new services(5, "E"));
        }

        public services[] GetAllServices()
        {
            return services_list.ToArray();
        }

        public services GetByName(string name)
        {
            services ch_service = services_list.Find(item => item.name == name);

            return ch_service;
        }
    }
    public class positions_repository : IBasePositionRepository
    {
        public List<positions> positions_list = new List<positions>();


        public positions_repository()
        {
            positions_list.Add(new positions(1, "Al"));
            positions_list.Add(new positions(2, "Bl"));
            positions_list.Add(new positions(3, "Cl"));
            positions_list.Add(new positions(4, "Dl"));
            positions_list.Add(new positions(5, "El"));
        }
        public positions GetByName(string name)
        {
            positions ch_position = positions_list.Find(item => item.name == name);

            return ch_position;
        }
    }

    public class equipment_class_repository : IBaseEquipmentClassRepository
    {
        public List<equipment_class> equipment_class_list = new List<equipment_class>();


        public equipment_class_repository()
        {
            equipment_class_list.Add(new equipment_class(1, "Al"));
            equipment_class_list.Add(new equipment_class(2, "Bl"));
            equipment_class_list.Add(new equipment_class(3, "Cl"));
            equipment_class_list.Add(new equipment_class(4, "Dl"));
            equipment_class_list.Add(new equipment_class(5, "El"));
        }

        public equipment_class[] GetAllClasses()
        {
            return equipment_class_list.ToArray();
        }

        public equipment_class GetByName(string name)
        {
            equipment_class ch_equipment_class = equipment_class_list.Find(item => item.name == name);

            return ch_equipment_class;
        }
    }

    public class equipment_repository : IBaseEquipmentRepository
    {
        public equipment[] GetAllEquipment()
        {
            throw new NotImplementedException();
        }

        public equipment[] GetAllEquipmnetForClass(int id)
        {
            throw new NotImplementedException();
        }

        /*
public List<equipment> equipment_list = new List<equipment>();


public equipment_repository()
{
   equipment_list.Add(new equipment(1, "Al"));
   equipment_list.Add(new equipment(2, "Bl"));
   equipment_list.Add(new equipment(3, "Cl"));
   equipment_list.Add(new equipment(4, "Dl"));
   equipment_list.Add(new equipment(5, "El"));
}*/
        public equipment GetByName(string name)
        {
            throw new NotImplementedException();
            /*
            equipment ch_equipment = equipment_list.Find(item => item.series == name);

            return ch_equipment;
            */
        }
        
    }

    public class employees_repository : IBaseEmployeesRepository
    {
        /* public employees GetById(int id)
         {
         }
         /*
         public List<employees> employees_list = new List<employees>();


         public employees_repository()
         {
             employees_list.Add(new employees(1, "Al"));
             employees_list.Add(new employees(2, "Bl"));
             employees_list.Add(new employees(3, "Cl"));
             employees_list.Add(new employees(4, "Dl"));
             employees_list.Add(new employees(5, "El"));
         }
         public employees GetById(int id)
         {
             employees ch_employee = status_list.Find(item => item.name == name);

             return ch_employee;
         }
         */
        public employees GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

}
