using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    /// <summary>
    /// Model CarService opisuje przegląd techniczny samochodu w bazie danych
    /// </summary>
    public class CarService
    {
        /// <summary>
        /// Klucz głowny modelu Car
        /// </summary>
        [Key]
        public int CarServiceId { get; set; }

        /// <summary>
        /// Wlaściwość From określa czas od którego ważny jest przegląd techniczny
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Wlaściwość To określa czas, do którego ważny przegląd techniczny
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Klucz obcy modelu Car
        /// </summary>
        [ForeignKey("Car")]
        public int CarId { get; set; }

        /// <summary>
        /// Zależność do klucza obcego Car
        /// </summary>
        public virtual Car Car { get; set; }
    }
}
