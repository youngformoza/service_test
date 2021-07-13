using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;

namespace DB_Connections.Interfaces
{
    interface IBaseRequestsRepository
    {
        requests[] GetRequests();

        requests GetById(int id);

        requests AddRequest (requests request);

        requests Update(requests request);

        void Delete(requests request);
    }
}
