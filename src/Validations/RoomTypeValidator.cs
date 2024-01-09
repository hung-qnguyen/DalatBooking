using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using FluentValidation;

namespace BookingApp.Validations
{
    public class RoomTypeValidator: AbstractValidator<RoomType>
    {
        public RoomTypeValidator()
        {
            RuleFor(roomType=>roomType.Type).NotEqual("hung").WithMessage("must not be 'hung'");
        }
    }
}