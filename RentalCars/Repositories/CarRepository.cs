using Microsoft.EntityFrameworkCore;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{
    public class CarRepository
    {

        private readonly RentalCarDbContext db = null;

        public CarRepository()
        {
            db = new RentalCarDbContext();
        }

        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        public Car GetFull(int id)
        {
            return db.Cars
                .Include(c => c.CarService)
                .Include(r => r.Rental)
                .Where(x => x.Id == id).First();
        }

        public List<Car> GetAll()
        {
            return db.Cars.Include(c => c.CarService).ToList();
        }

        public void AddCar(Car car, CarService carService)
        {
            if (car != null && carService != null)
            {
                car.CarService = carService;
                db.Cars.Add(car);
                db.SaveChanges();

            }
        }

        public void UpdateCar(int Id, Car car)
        {
            var carId = this.GetFull(Id);

            if (carId.Rental != null)
            {
                throw new InvalidOperationException();
            }

            if (carId != null)
            {
                carId.Marka = car.Marka;
                carId.Model = car.Model;
                carId.RegistrationNumber = car.RegistrationNumber;
                carId.Cost = car.Cost;

                carId.CarService = car.CarService;

                db.SaveChanges();
            }
        }


        public void RemoveCar(Car car)
        {

            var newCar = db.Cars.Where(c => c.Rental == null && c.Id == car.Id)
                    .First();

            if (newCar != null)
            {
                db.Cars.Remove(newCar);
                db.SaveChanges();
            }
        }

    }
}
