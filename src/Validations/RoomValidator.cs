using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using FluentValidation;

namespace BookingApp.Validations
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(room => room.Occupancy)
                .LessThanOrEqualTo(room => room.Capacity)
                .WithMessage("must not be greater than Capacity");
        }
    }
}
