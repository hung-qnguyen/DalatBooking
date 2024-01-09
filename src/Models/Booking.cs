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
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Check-In Date")]
        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; } = DateTime.Now;

        [Display(Name = "Check-Out Date")]
        [DataType(DataType.DateTime)]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Total Price")]
        [Range(1, 100000000)]
        [DataType(DataType.Currency)]
        [
            Column(TypeName = "decimal(10,2)"),
            DisplayFormat(DataFormatString = "{0:,000,000.00}", ApplyFormatInEditMode = true)
        ]
        public decimal TotalPrice { get; set; }

        [MaxLength(300)]
        public string? Notes { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedAt { get; set; }

        [Display(Name = "Cancelled At")]
        [DataType(DataType.DateTime)]
        public DateTime CancelledAt { get; set; }

        [Display(Name = "Booking Status")]
        public BookingStatus Status { get; set; }

        public enum BookingStatus
        {
            Cancelled,
            Paid
        }

        [Display(Name = "Guest")]
        public int GuestId { get; set; }

        [ForeignKey("GuestId"), ValidateNever]
        public Guest? Guest { get; set; }

        [ValidateNever, Display(AutoGenerateField = false)]
        public ICollection<RoomBooked>? RoomBooked { get; }

        public override string ToString()
        {
            return CheckIn.ToLongDateString();
        }
    }
}
