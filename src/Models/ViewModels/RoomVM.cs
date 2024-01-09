using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingApp.Models.ViewModels
{
    public class RoomVM
    {
        public Room Room { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RoomTypeList { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem>? HotelList { get; set; }
    }
}