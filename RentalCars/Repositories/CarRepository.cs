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

        public List<Car> GetAll()
        {
            return db.Cars.ToList();
        }

        public void AddCar(Car car, CarService carService)
        {
            if (car != null && carService != null)
            {
                car.CarService = carService;
                db.Cars.Add(car);
                db.SaveChanges();

                db.SaveChanges();

            }
        }

       

    }
}
