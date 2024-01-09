using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;
using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Data
{
    public class HotelContext : IdentityDbContext<IdentityUser>
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options) { }

        public DbSet<HotelCategory> HotelCategory { get; set; } = default!;
        public DbSet<Hotel> Hotel { get; set; } = default!;

        public DbSet<RoomType> RoomType { get; set; } = default!;
        public DbSet<Room> Room { get; set; } = default!;
        public DbSet<RoomBooked> RoomBooked { get; set; } = default!;
        public DbSet<Guest> Guest { get; set; } = default!;
        public DbSet<Booking> Booking { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
