using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using BookingApp.Data;
using BookingApp.Repository.IRepository;

namespace BookingApp.Repository
{
    public class RoomRepository: Repository<Room>, IRoomRepository
    {
        private readonly HotelContext _context;
        public RoomRepository(HotelContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Room obj)
        {
            _context.Room.Update(obj);
        }
    }
}