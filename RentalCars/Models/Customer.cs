using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    /// <summary>
    /// Model Customer określa klienta wynajmującego w bazie danych
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Klucz głowny modelu Customer
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Wlaściwość FirstName opisuje imie klienta, pole wymagane ograniczone maksymalnie do 30 znaków
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// Wlaściwość SecondName opisuje nazwisko klienta, pole wymagane ograniczone maksymalnie do 30 znaków
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        /// <summary>
        /// Wlaściwość PhoneNumber opisuje numer telefonu klienta, pole ograniczone do 9 znaków
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Zależność klucza obcego Rental
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
