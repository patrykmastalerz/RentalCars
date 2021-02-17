using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCars.Models
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzanie połączeniem z bazą danych
    /// </summary>
    public class RentalCarDbContext : DbContext
    {
        /// <summary>
        /// Właściwość mapujaca Cars na tabele w bazie danych 
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Właściwość mapujaca Customers na tabele w bazie danych 
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Właściwość mapujaca CarServices na tabele w bazie danych 
        /// </summary>
        public DbSet<CarService> CarServices { get; set; }

        /// <summary>
        /// Właściwość mapujacae Rentals na tabele w bazie danych 
        /// </summary>
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RentalCarDb;Trusted_Connection=True;");
        }

    }
}
