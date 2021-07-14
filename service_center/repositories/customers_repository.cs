using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;

namespace service_center.repositories
{
    public class customers_repository : IBaseCustomersRepository
    {
        public List<customer> customer_list = new List<customer>();


        public customers_repository()
        {
            customer_list.Add(new customer(1, "Al", "eng", DateTime.Now, "koko", 80808));
            customer_list.Add(new customer(2, "Bl", "man", DateTime.Now, "kiko", 88808));
            customer_list.Add(new customer(3, "Cl", "eng", DateTime.Now, "keko", 80888));
            customer_list.Add(new customer(4, "Dl", "dir", DateTime.Now, "kvko", 80800));
            customer_list.Add(new customer(5, "El", "eng", DateTime.Now, "kqko", 80000));
        }
        
        public void AddCustomer(customer new_customer)
        {
            int id = customer_list.Count + 1;
            customer_list.Add(new customer(id, new_customer.name, new_customer.position_cus, DateTime.Now, new_customer.mail, new_customer.phone));
            //customer_list.Insert(id - 1, new customer(id, customer.name, customer.position_cus, DateTime.Now, customer.mail, customer.phone));

            foreach (customer customer in customer_list)
            {
                Console.WriteLine(customer);
            }
        }

        public void Delete(customer ch_customer)
        {
            customer_list.Remove(ch_customer);
            foreach (customer customer in customer_list)
            {
                Console.WriteLine(customer);
            }
        }

        public customer GetById(int id)
        {
            customer ch_customer = customer_list.Find(item => item.id_cus == id);

            return ch_customer;
        }

        public customer[] GetCustomers()
        {
           /* foreach (customer ch_customer in customer_list)
            {
                Console.WriteLine(ch_customer);
            }*/
            return customer_list.ToArray();
        }

        public void Update(customer ch_customer)
        {
            customer_list.
                FindAll(item => item.id_cus == ch_customer.id_cus).
                ForEach(x => { x.name = ch_customer.name; x.position_cus = ch_customer.position_cus; x.mail = ch_customer.mail; x.phone = ch_customer.phone; } );
        }
    }
}
