using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using BookingApp.Models;

namespace BookingApp.Models
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100), Required]
        public string Name { get; set; }

        [DataType(DataType.Currency), Range(1, 100000000)]
        [
            Column(TypeName = "decimal(10,2)"),
            DisplayFormat(DataFormatString = "{0:,000,000.00}", ApplyFormatInEditMode = false)
        ]
        public decimal PricePerNight { get; set; }

        [DefaultValue(false)]
        public bool Booked { get; set; }

        [DataType("Markdown"), MaxLength(3000)]
        public string? Details { get; set; }

        [Range(1, 10), Required]
        public int Capacity { get; set; }

        [Range(0, 10), DefaultValue(0)]
        public int? Occupancy { get; set; }

        [Display(Name = "Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("HotelId"), ValidateNever]
        public Hotel? Hotel { get; set; }

        [Display(Name = "Room Type")]
        public int RoomTypeId { get; set; }

        [ForeignKey("RoomTypeId"), ValidateNever]
        public RoomType? RoomType { get; set; }

        [ValidateNever, Display(AutoGenerateField = false)]
        public ICollection<RoomBooked>? RoomBooked { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
