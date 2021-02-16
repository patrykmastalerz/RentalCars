using Microsoft.EntityFrameworkCore;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{
    /// <summary>
    /// Repozytirium odpowiedzialne za odczytywanie, zapisywanie, usuwanie oraz modyfikowanie obiektów typu Rental w bazie danych
    /// </summary>
    public class RentalRepository
    {
        private readonly RentalCarDbContext db = null;

        /// <summary>
        /// Inicjalizuje obiekt RentalRepository oraz inicjalizuje obiekt RentalCarDbContext.
        /// </summary>
        public RentalRepository()
        {
            db = new RentalCarDbContext();
        }

        /// <summary>
        /// Metoda służąca do znalezienia obiektu typu Rental w bazie danych
        /// </summary>
        /// <param name="id">Integer użyty do znalezienia obiektu o numerze</param>
        /// <returns>Obiekt typu Rental, zwrócony z bazy danych</returns>
        public Rental Get(int id)
        {
            return db.Rentals.Find(id);
        }

        /// <summary>
        /// Metoda słuząca do znalezenia w bazie danych obiektów Rental wraz z powiązanymi z nim jednostkami: Customer oraz Car
        /// </summary>
        /// <returns>Lista obiektów typu Rental, znalezione w bazie danych</returns>
        public List<Rental> GetAll()
        {
            return db.Rentals.Include(c => c.Customers).Include(c => c.Cars).ToList();
        }

        /// <summary>
        /// Metoda służąca do dodania obiektu Rental do bazy danych i przypisania do niego obiektów Customer oraz Car. 
        /// Jeżeli znalezione obiekty po ID: Car oraz Customer nie istnieją w bazie, zwrócony jest wyjątek InvalidOperationException, 
        /// jeśli zaś w bazie danych istnieje już rezerwacja na dany samochód to zostanie zwrócony wyjątek InvalidOperationException 
        /// </summary>
        /// <param name="rental">Obiekt typu Rental, który zostanie zapisany do bazy danych oraz użyty do znalezienia obiektów Car oraz Customer, 
        /// które zostaną do niego przypisane</param>
        public void AddRental(Rental rental)
        {
            var newRental = db.Rentals.Where(x => x.CarId == rental.CarId).FirstOrDefault();


            
            var newCustomer = db.Customers.Find(rental.CustomerId);
            var newCar = db.Cars.Find(rental.CarId);


            if (newCar == null)
            {
                throw new InvalidOperationException("Nie ma takiego samochodu");
            }

            if (newRental != null)
            {
                throw new InvalidOperationException("Jest już rezerwacja na ten samochód");
            }

            if (newCustomer == null)
            {
                throw new InvalidOperationException("Nie ma takiego użytkownika");
            }

            if (rental != null)
            {

                rental.Customers = newCustomer;
                rental.Cars = newCar;
                db.Rentals.Add(rental);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Metoda służąca do usunięcia obiektu Rental z bazy danych
        /// </summary>
        /// <param name="id">Integer użyty do znalezenia obiektu Rental, który zostanie usunięty z bazy danych</param>
        public void RemoveRental(int id)
        {
            var rental = this.Get(id);
            if (rental != null)
            {
                db.Rentals.Remove(rental);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Metoda służąca do zmodyfikowania obiektu Rental w bazie danych, znalezionego na podstawie numeru ID. 
        /// Metoda pozwala na modyfikowanie takich pol jak From, To, Cost oraz przypisanie innego pojazdu do Rental. 
        /// Jeśli istnieje już rezerwacja na samochód lub samochód nie istnieje zostaje zgłoszony wyjątek InvalidOperationException
        /// </summary>
        /// <param name="id">Integer użyty do znalezenia obiektu Rental w bazie danych</param>
        /// <param name="rental">Obiekt typu Rental, który zostanie użyty do przypisania nowych wartości do pol oraz znalezenie obiektu obiektu Car, 
        /// za pomocą zawartego ID w tym obiekcie</param>
        public void UpdateRental(int id, Rental rental)
        {
            var toUpdate = this.Get(id);
            var newRental = db.Rentals.Where(x => x.CarId == rental.CarId).FirstOrDefault();

            var newCar = db.Cars.Find(rental.CarId);


            if (newCar == null)
            {
                throw new InvalidOperationException("Nie ma takiego samochodu");
            }

            if (newRental != null)
            {
                throw new InvalidOperationException("Jest już rezerwacja na ten samochód");
            }


            if (rental != null)
            {

                toUpdate.From = rental.From;
                toUpdate.To = rental.To;
                toUpdate.Cost = rental.Cost;
                toUpdate.Cars = newCar;
                db.Rentals.Add(rental);
                db.SaveChanges();
            }
        }


    }
}
