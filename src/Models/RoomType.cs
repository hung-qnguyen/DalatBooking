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
    public class RoomType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100), Required]
        public string Type { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [ValidateNever, Display(AutoGenerateField = false), JsonIgnore]
        public ICollection<Room>? Rooms { get; }

        public override string ToString()
        {
            return Type;
        }
    }
}
