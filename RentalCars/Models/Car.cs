using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Marka { get; set; }
        [Required]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        [StringLength(8)]
        public string RegistrationNumber { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }


        public virtual Rental Rental { get; set; }

        public virtual CarService CarService { get; set; }
    }
}
