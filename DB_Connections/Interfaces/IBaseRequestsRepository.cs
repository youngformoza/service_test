using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseRequestsRepository
    {
        Request[] GetRequests();

        Request[] GetAllRequestsForCustomer(int idCustomer);

        Request GetById(int id);

        void AddRequest (Request request);

        void Update(Request request);

    }
}
