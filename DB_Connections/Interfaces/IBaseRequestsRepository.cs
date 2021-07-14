using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    public interface IBaseRequestsRepository
    {
        Request[] GetRequests();

        Request GetById(int id);

        int GetId(int currentRow);

        void AddRequest (Request request);

        void Update(Request request);

    }
}
