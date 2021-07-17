using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBasePositionRepository
    {
        positions GetByName(string name);
    }

    public interface IBaseVendorRepository
    {
        vendor GetByName(string name);
    }

    public interface IBaseStatusRepository
    {
        status GetByName(string name);
        status[] GetAllStatuses();
    }

    public interface IBaseServicesRepository
    {
        services GetByName(string name);
        services[] GetAllServices();
    }

    public interface IBaseEquipmentClassRepository
    {
        equipment_class GetByName(string name);
        equipment_class[] GetAllClasses();

    }

    public interface IBaseEquipmentRepository
    {
        equipment GetByName(string name);
        equipment[] GetAllEquipment();

        equipment[] GetAllEquipmnetForClass(int id);
    }

    public interface IBaseEmployeesRepository
    {
        employees GetById(int id);
    }
}
