using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentalCars.Models
{
    public class CarService
    {
        [Key]
        public int CarServiceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
