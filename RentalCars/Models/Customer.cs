using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Column(TypeName = "char")]
        [StringLength(9)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
