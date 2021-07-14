using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;

namespace service_center.repositories
{
    public class requests_repository : IBaseRequestsRepository
    {
        public List<request> requests_list = new List<request>();

        /*
        public requests_repository()
        {
            requests_list.Add(new request(1, DateTime.Now, "срочно", 1, 1, 1, 1, 1));
            requests_list.Add(new request(2, "Bl", "man", DateTime.Now, "kiko", 88808));
            requests_list.Add(new request(3, "Cl", "eng", DateTime.Now, "keko", 80888));
            requests_list.Add(new request(4, "Dl", "dir", DateTime.Now, "kvko", 80800));
            requests_list.Add(new request(5, "El", "eng", DateTime.Now, "kqko", 80000));
        }
        */

        public void AddRequest(request new_request)
        {
            int id = requests_list.Count + 1;
            requests_list.Add(new request(id, new_request.date_time_start, new_request.urgency, new_request.cus, new_request.eq, new_request.ser, new_request.recep, new_request.stat));
        }


        public request GetById(int id)
        {
            request ch_request = requests_list.Find(item => item.id_req == id);

            return ch_request; 
        }

        public request[] GetRequests()
        {
            return requests_list.ToArray();
        }

        public void Update(request ch_request)
        {
            requests_list.
                FindAll(item => item.id_req == ch_request.id_req).
                ForEach(x => { x.ser = ch_request.ser;  });
        }
    }
}
