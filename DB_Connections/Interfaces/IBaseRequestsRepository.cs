using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseRequestsRepository
    {
        request[] GetRequests();

        request GetById(int id);

        void AddRequest (request request);

        void Update(request request);

    }
}
