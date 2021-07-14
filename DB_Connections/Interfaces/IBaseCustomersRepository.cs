using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseCustomersRepository
    {
        Customer[] GetCustomers();

        Customer GetById(int id);

        void AddCustomer(Customer new_customer);

        void Update(Customer ch_customer);

        void Delete(Customer ch_customer);
    }
}
