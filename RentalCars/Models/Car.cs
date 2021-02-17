using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    /// <summary>
    /// Model Car opisujacy dane odnośnie każdego z samochodów przechowywanych w bazie danych
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Klucz głowny modelu Car
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Pole Marka, określa marke samochodu, pole jest wymagane i ograniczone do maksymalnie 20 znaków
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Marka { get; set; }

        /// <summary>
        /// Pole Model, określa model samochodu, pole jest wymagane i ograniczone maksymalnie do 20 znaków
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Model { get; set; }

        /// <summary>
        /// Pole RegistrationNumber, określa numer rejestracyjny każdego z samochodów, ograniczony jest do 8 znaków, pole jest wymagane
        /// </summary>
        [Required]
        [StringLength(8)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Pole Cost, określa cene za godzine wynajmu samochodu, pole jest wymagane
        /// </summary>
        [Required]
        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        /// <summary>
        /// Zależność do klucza obcego Rental
        /// </summary>
        public virtual Rental Rental { get; set; }

        /// <summary>
        /// Zależność do klucza obcego CarService
        /// </summary>
        public virtual CarService CarService { get; set; }
    }
}
