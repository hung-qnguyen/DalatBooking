using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingApp.Models
{
    public class RoomBooked
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Currency), Range(1, 100000000)]
        [
            Column(TypeName = "decimal(10,2)"),
            DisplayFormat(DataFormatString = "{0:,000,000.00}", ApplyFormatInEditMode = true)
        ]
        public decimal Price { get; set; }

        [Range(1, 5)]
        public int Guests { get; set; }

        [Display(Name = "Booking")]
        public int BookingId { get; set; }

        [ForeignKey("BookingId"), ValidateNever]
        public Booking? Booking { get; set; }

        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId"), ValidateNever]
        public Room? Room { get; set; }
    }
}
