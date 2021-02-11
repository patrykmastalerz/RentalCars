using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        [ForeignKey("Customer")]

        public int CustomerId { get; set; }

        public virtual Customer Customers { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Cars { get; set; }


    }
}
