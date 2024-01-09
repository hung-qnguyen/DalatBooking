using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingApp.Models
{
    public class HotelCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [ValidateNever]
        [Display(AutoGenerateField = false)]
        public ICollection<Hotel>? Hotels { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
