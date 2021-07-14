using System;
using System.Collections.Generic;
using System.Text;
using DB_Connections.Entities;
using DB_Connections.Interfaces;

namespace service_center.repositories
{
    public class CustomersRepository : IBaseCustomersRepository
    {
        private static readonly List<Customer> CustomerList = new List<Customer>();

        public CustomersRepository()
        {
            CustomerList.Add(new Customer(1, "Al", "eng", DateTime.Now, "koko", 80808));
            CustomerList.Add(new Customer(2, "Bl", "man", DateTime.Now, "kiko", 88808));
            CustomerList.Add(new Customer(3, "Cl", "eng", DateTime.Now, "keko", 80888));
            CustomerList.Add(new Customer(4, "Dl", "dir", DateTime.Now, "kvko", 80800));
            CustomerList.Add(new Customer(5, "El", "eng", DateTime.Now, "kqko", 80000));
        }
        
        public void AddCustomer(Customer new_customer)
        {
            int id = CustomerList.Count + 1;
            CustomerList.Add(new Customer(id, new_customer.name, new_customer.position_cus, DateTime.Now, new_customer.mail, new_customer.phone));
            //customer_list.Insert(id - 1, new customer(id, customer.name, customer.position_cus, DateTime.Now, customer.mail, customer.phone));

            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer);
            }
        }

        public void Delete(Customer ch_customer)
        {
            CustomerList.Remove(ch_customer);
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer);
            }
        }

        public Customer GetById(int id)
        {
            Customer ch_customer = CustomerList.Find(item => item.id_cus == id);

            return ch_customer;
        }

        public Customer[] GetCustomers()
        {
           /* foreach (customer ch_customer in customer_list)
            {
                Console.WriteLine(ch_customer);
            }*/
            return CustomerList.ToArray();
        }

        public void Update(Customer ch_customer)
        {
            CustomerList.
                FindAll(item => item.id_cus == ch_customer.id_cus).
                ForEach(x => { x.name = ch_customer.name; x.position_cus = ch_customer.position_cus; x.mail = ch_customer.mail; x.phone = ch_customer.phone; } );
        }
    }
}
