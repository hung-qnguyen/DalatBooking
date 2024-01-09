using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingApp.Models
{
    public class Guest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Please Enter Valid Phone Number!")]
        [StringLength(13), Required]
        public string Phone { get; set; }

        [StringLength(1000), Required]
        public string? Address { get; set; }

        [Range(18, 130), Required]
        public int Age { get; set; }

        public GuestGender Gender { get; set; }

        public enum GuestGender
        {
            Male,
            Female
        }

        [StringLength(200), Required]
        public string Country { get; set; }

        [ValidateNever, Display(AutoGenerateField = false)]
        public ICollection<Booking>? Bookings { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
