using Microsoft.EntityFrameworkCore;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{
    public class RentalRepository
    {
        private readonly RentalCarDbContext db = null;

        public RentalRepository()
        {
            db = new RentalCarDbContext();
        }

        public Rental Get(int id)
        {
            return db.Rentals.Find(id);
        }

        public List<Rental> GetAll()
        {
            return db.Rentals.Include(c => c.Customers).Include(c => c.Cars).ToList();
        }

        public void AddRental(Rental rental)
        {
            var newCustomer = db.Customers.Find(rental.CustomerId);
            var newCar = db.Cars.Find(rental.CarId);
            var newRental = db.Rentals
                .Include(c => c.Cars)
                .Where( i => i.CarId == newCar.Id)
                .First();

            if (newCar == null)
            {
                throw new InvalidOperationException();
            }

            if (newRental.Cars == newCar)
            {
                throw new InvalidOperationException();
            }

            if (newCustomer == null)
            {
                throw new InvalidOperationException();
            }

            if (rental != null)
            {

                rental.Customers = newCustomer;
                rental.Cars = newCar;
                db.Rentals.Add(rental);
                db.SaveChanges();
            }
        }

    }
}
