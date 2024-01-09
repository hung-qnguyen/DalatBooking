using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingApp.Models
{
    public class Hotel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [Required, MaxLength(100), MinLength(3)]
        // [Display(Name = "Hotel Name")]
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [StringLength(1000), Required]
        public string? Address { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Please Enter Valid Phone Number!")]
        [StringLength(13), Required]
        public string? Phone { get; set; }

        // [Display(Name = "Hotel Email")]
        [RegularExpression(
            @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            ErrorMessage = "Please Enter Valid Email Address!"
        )]
        [StringLength(100), Required]
        public string? Email { get; set; }

        [Range(1, 5)]
        [Column(TypeName = "decimal(2,1)")]
        public decimal? Ratings { get; set; }

        [ValidateNever]
        public byte[]? Image { get; set; }

        [ForeignKey("CategoryId"), ValidateNever]
        public HotelCategory? Category { get; set; }

        [ValidateNever, Display(AutoGenerateField = false), JsonIgnore]
        public ICollection<Room>? Rooms { get; }

        public override string ToString()
        {
            return Name;
        }

        public string ToUrl()
        {
            if (Image==null)
                return "";
            else
                return "data:image;base64," + Convert.ToBase64String(Image!);
        }
    }
}
