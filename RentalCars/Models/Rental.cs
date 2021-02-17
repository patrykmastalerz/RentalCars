using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    /// <summary>
    /// Model Rental opisuje wynajem samochodu przez klienta w bazie danych
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Klucz głowny klasy Rental
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Wlaściwość From, określa okres od kiedy ważne jest wynajęcie, pole wymagane
        /// </summary>
        [Required]
        public DateTime From { get; set; }


        /// <summary>
        /// Wlaściwość From, określa okres dp kiedy ważne jest wynajęcie, pole wymagane
        /// </summary>
        [Required]
        public DateTime To { get; set; }

        /// <summary>
        /// Wlaściwość Cost, określa kwote wynajmu
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }


        /// <summary>
        /// Klucz obcy Modelu Customer
        /// </summary>
        [ForeignKey("Customer")]

        public int CustomerId { get; set; }

        /// <summary>
        /// Zależnosc do klucz obcego Customer
        /// </summary>
        public virtual Customer Customers { get; set; }
        
        /// <summary>
        /// Klucz obcy Modelu Car
        /// </summary>
        [ForeignKey("Car")]
        public int CarId { get; set; }

        /// <summary>
        /// Zależnosc do klucz obcego Car
        /// </summary>
        public virtual Car Cars { get; set; }


    }
}
