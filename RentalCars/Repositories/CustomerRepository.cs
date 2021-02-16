using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{
    /// <summary>
    /// Repozytirium odpowiedzialne za odczytywanie, zapisywanie oraz modyfikowanie obiektów typu Customer z bazy danych
    /// </summary>
    public class CustomerRepository
    {
        private readonly RentalCarDbContext db = null;

        /// <summary>
        /// Inicjalizuje obiekt CustomerRepository oraz inicjalizuje obiekt RentalCarDbContext.
        /// </summary>
        public CustomerRepository()
        {
            db = new RentalCarDbContext();
        }

        /// <summary>
        /// Metoda służąca do znalezienia obiektu Customer w bazie danych
        /// </summary>
        /// <param name="id">Integer użyty do znalezienia obiektu w bazie danych</param>
        /// <returns>Obiekt typu Customer, zwrócony z bazy danych</returns>
        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        /// <summary>
        /// Metoda służąca do znalezienia wszystkich obiektów typu Customer
        /// </summary>
        /// <returns>Zwraca liste obiektów typu Customer znalezionych w bazie danych</returns>
        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        /// <summary>
        /// Metoda służąca do dodania obietu typu Customer do bazy danych
        /// </summary>
        /// <param name="customer">Obiekt typu Customer, który zostanie dodany do bazy danych</param>
        public void AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Metoda służąca do zmodyfikowaia obiektu Customer, znalezionego w bazie danych za pomocą ID. Metoda pozwala modyfikować takie pola jak FirstName, SecondName oraz PhoneNumber
        /// </summary>
        /// <param name="Id">Integer użyty do znalezienia obiektu Customer w bazie danych</param>
        /// <param name="customer">Obiekt typu Customer, którego pola zostaną przypisane do modyfikowanego obiektu Customer w bazie danych</param>
        public void UpdateCustomer(int Id, Customer customer)
        {
            var customerId = this.Get(Id);
            if (customerId != null)
            {
                customerId.FirstName = customer.FirstName;
                customerId.SecondName = customer.SecondName;
                customerId.PhoneNumber = customer.PhoneNumber;

                db.SaveChanges();
            }
        }

    }
}
