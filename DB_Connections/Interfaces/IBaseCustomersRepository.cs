using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseCustomersRepository
    {
        customers[] GetCustomers();

        customers GetById(int id);

        customers AddCustomer(customers customer);

        customers Update(customers customer);

        void Delete(customers customer);
    }
}
