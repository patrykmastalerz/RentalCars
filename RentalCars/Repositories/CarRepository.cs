using Microsoft.EntityFrameworkCore;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalCars.Repositories
{

    /// <summary>
    /// Repozytorium odpowiedzialne za zapisywanie, odczytywanie, modyfikowanie oraz usuwanie obiektu Car do bazy danych
    /// </summary>
    public class CarRepository
    {

        private readonly RentalCarDbContext db = null;

        /// <summary>
        /// Inicjalizuje obiekt CarRepository oraz inicjalizuje obiekt RentalCarDbContext 
        /// </summary>
        public CarRepository()
        {
            db = new RentalCarDbContext();
        }

        /// <summary>
        /// Metoda służąca do znależenia obiektu Car za pomocą ID
        /// </summary>
        /// <param name="id">Integer użyty do znalezenia w bazie danych obiektu o podanym Id</param>
        /// <returns>Zwraca obiekt Car</returns>
        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        /// <summary>
        /// Metoda słuząca do znalezenia w bazie danych obiektu Car wraz z powiązanymi z nim jednostkami: Rental oraz CarService, przy użyciu ID
        /// </summary>
        /// <param name="id">Integer użyty do znalezenia w bazie danych obiektu o podanym Id</param>
        /// <returns>Zwraca obiekt Car z powiązanymi jednostkami: Rental oraz CarService</returns>
        public Car GetFull(int id)
        {
            return db.Cars
                .Include(c => c.CarService)
                .Include(r => r.Rental)
                .Where(x => x.Id == id).First();
        }

        /// <summary>
        /// Metoda służąca do znalezenia wszystkich obiektów Car w bazie danych i powiązanymi z nimi jednostkami CarService
        /// </summary>
        /// <returns>Zwraca liste obiektów Car</returns>
        public List<Car> GetAll()
        {
            return db.Cars.Include(c => c.CarService).ToList();
        }

        /// <summary>
        /// Metoda służąca do dodania obiektu Car do bazy danych i przypisania do niego obiektu CarService
        /// </summary>
        /// <param name="car">Obiekt typu Car, który zostanie dodany do bazy danych</param>
        /// <param name="carService">Obiekt typu CarService, który zostanie przypisany do obiektu Car</param>
        public void AddCar(Car car, CarService carService)
        {
            if (car != null && carService != null)
            {
                car.CarService = carService;
                db.Cars.Add(car);
                db.SaveChanges();

            }
        }

        /// <summary>
        /// Metoda służąca do zmodyfikowania obiektu Car znalezionego w bazie danych za pomocą ID. Metoda pozwala na modyfikacje wartości dla takich pól jak Marka, Model, Cost oraz RegistrationNumber oraz obiektu CarService
        /// </summary>
        /// <param name="Id">Integer użyty do wyszukania obiektu w bazie danych</param>
        /// <param name="car">Obiekt Car, którego zawartość zostanie przypisana do znalezionego obiektu w bazie danych</param>
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

        /// <summary>
        /// Metoda służąca do usunięcia obiektu Car z bazy danych, tylko takiego który nie ma przypisanej relacji do obiektu Rental
        /// </summary>
        /// <param name="car">Obiekt Car, który zostanie usunięty w bazie danych. Użyty do znalezienia za pomoca jego ID obiektu w bazie danych oraz jego relacji do obiektu Rental</param>
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
