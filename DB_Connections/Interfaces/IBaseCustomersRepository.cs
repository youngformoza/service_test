using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseCustomersRepository
    {
        customer[] GetCustomers();

        customer GetById(int id);

        void AddCustomer(customer new_customer);

        void Update(customer ch_customer);

        void Delete(customer ch_customer);
    }
}
