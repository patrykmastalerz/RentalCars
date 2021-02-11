using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{
    public class CustomerRepository
    {
        private readonly RentalCarDbContext db = null;

        public CustomerRepository()
        {
            db = new RentalCarDbContext();
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

    }
}
