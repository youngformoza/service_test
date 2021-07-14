using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;

namespace service_center.repositories
{
    public class RequestsRepository : IBaseRequestsRepository
    {
        public List<Request> RequestList = new List<Request>();

        
        public RequestsRepository()
        {
           //var customersRep = new CustomersRepository();

           //var customers = customersRep.GetCustomers();



           // requests_list.Add(new request(1, DateTime.Now, "срочно", customers[0], ));
            //requests_list.Add(new request(2, "Bl", "man", DateTime.Now, "kiko", 88808));
            //requests_list.Add(new request(3, "Cl", "eng", DateTime.Now, "keko", 80888));
            //requests_list.Add(new request(4, "Dl", "dir", DateTime.Now, "kvko", 80800));
            //requests_list.Add(new request(5, "El", "eng", DateTime.Now, "kqko", 80000));
        }
        

        public void AddRequest(Request new_request)
        {
            int id = RequestList.Count + 1;
            RequestList.Add(new Request(id, new_request.date_time_start, new_request.urgency, new_request.cus, new_request.eq, new_request.ser, new_request.recep, new_request.stat));
        }


        public Request GetById(int id)
        {
            return RequestList.Find(item => item.id_req == id);
        }

        public Request[] GetRequests()
        {
            return RequestList.ToArray();
        }

        public void Update(Request ch_request)
        {
            var requestToUpdate = GetById(ch_request.id_req);

            requestToUpdate.ser = ch_request.ser;
            requestToUpdate.stat = ch_request.stat;

        }

        public int GetId(int currentRow)
        {
            return RequestList[currentRow].id_req;
        }

    }
}
